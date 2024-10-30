var localStorage = {};

// Set proxy icon based on mode
function setProxyIcon() {
    chrome.proxy.settings.get(
        { 'incognito': false },
        function(config) {
            var icon = { path: "images/off.png" };
            if (config.value && config.value.mode !== "direct") {
                icon.path = "images/on.png";
            }
            chrome.action.setIcon(icon);
        }
    );
}

// Callback for proxy authentication
async function callbackFn(details, cb) {
    console.log("HTTP Proxy onAuthRequired");

    if (!localStorage.proxySetting) {
        await getLocalStorage();
    }

    var proxySetting = JSON.parse(localStorage.proxySetting || '{}');
    var auth = proxySetting.auth || {};

    if (auth.user && auth.pass) {
        cb({ authCredentials: { username: auth.user, password: auth.pass } });
    } else {
        cb({});
    }
}

// Retrieve local storage settings
async function getLocalStorage() {
    return chrome.storage.local.get(null).then(result => {
        Object.assign(localStorage, result);
        return result;
    });
}

// Listener for authentication requests
chrome.webRequest.onAuthRequired.addListener(
    callbackFn,
    { urls: ["<all_urls>"] },
    ['asyncBlocking']
);

// Initialize proxy icon and storage settings on install
chrome.runtime.onInstalled.addListener(async (details) => {
    var store = await getLocalStorage();
    if (!store.proxySetting) {
        var defaultProxySetting = {
            http_host: '103.177.108.235',     // Set your HTTP proxy host
            http_port: '6789',                  // Set your HTTP proxy port
            auth: {
                user: 'xwci1j2w',           // Set username if needed
                pass: 'xWcI1j2w'            // Set password if needed
            },
            bypasslist: '<local>,192.168.0.0/16,172.16.0.0/12,10.0.0.0/8',
            proxy_rule: 'singleProxy'
        };
        localStorage.proxySetting = JSON.stringify(defaultProxySetting);
        await chrome.storage.local.set(localStorage);
    }

    if (details.reason === "install") {
        chrome.tabs.create({ url: 'options.html' });
    }
    setProxyIcon();
});

// Set proxy icon on startup
chrome.runtime.onStartup.addListener(setProxyIcon);

console.log("Service worker initialized");
