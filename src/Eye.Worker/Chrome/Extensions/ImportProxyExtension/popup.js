// Save proxy settings from form inputs
function saveProxyHttpSettings(httpHost, httpPort, username, password) {
    if (!httpHost || !httpPort) {
        alert("Please enter both the HTTP proxy host and port.");
        return;
    }

    const proxySetting = {
        http_host: httpHost,
        http_port: httpPort,
        auth: { user: username, pass: password },
        bypasslist: "<local>,192.168.0.0/16,172.16.0.0/12,10.0.0.0/8"
    };

    localStorage.proxySetting = JSON.stringify(proxySetting);
    chrome.storage.local.set({ proxySetting: JSON.stringify(proxySetting) }, () => {
        console.log("Proxy settings saved");
    });
}

function saveProxySock5Settings(socksHost, socksPort, username, password)
{
    // const socksHost = document.getElementById('socks_host').value;
    // const socksPort = document.getElementById('socks_port').value;
    // const username = document.getElementById('username').value;
    // const password = document.getElementById('password').value;

    if (!httpHost && !socksHost) {
        alert("Please enter at least one proxy host (HTTP or SOCKS5).");
        return;
    }

    const proxySetting = {
        socks_host: socksHost,
        socks_port: socksPort,
        auth: { user: username, pass: password },
        bypasslist: "<local>,192.168.0.0/16,172.16.0.0/12,10.0.0.0/8"
    };

    localStorage.proxySetting = JSON.stringify(proxySetting);
    chrome.storage.local.set({ proxySetting: JSON.stringify(proxySetting) }, () => {
        console.log("Proxy settings saved");
    });
}

// Set HTTP Proxy based on saved settings
function httpProxy() {
    const proxySetting = JSON.parse(localStorage.proxySetting || '{}');
    const httpHost = proxySetting.http_host;
    const httpPort = proxySetting.http_port;
    const bypasslist = proxySetting.bypasslist ? proxySetting.bypasslist.split(',') : [];

    if (!httpHost || !httpPort) {
        console.error("HTTP proxy host or port not set.");
        return;
    }

    const config = {
        mode: 'fixed_servers',
        rules: {
            singleProxy: {
                scheme: 'http',
                host: httpHost,
                port: parseInt(httpPort)
            },
            bypassList: bypasslist
        }
    };

    chrome.proxy.settings.set({ value: config, scope: 'regular' }, () => {
        console.log("HTTP proxy set to:", httpHost + ":" + httpPort);
    });

    chrome.action.setIcon({ path: "images/on.png" });
}

// Set SOCKS5 Proxy based on saved settings
function socksProxy() {
    const proxySetting = JSON.parse(localStorage.proxySetting || '{}');
    const socksHost = proxySetting.socks_host;
    const socksPort = proxySetting.socks_port;
    const bypasslist = proxySetting.bypasslist ? proxySetting.bypasslist.split(',') : [];

    if (!socksHost || !socksPort) {
        console.error("SOCKS5 proxy host or port not set.");
        return;
    }

    const config = {
        mode: 'fixed_servers',
        rules: {
            singleProxy: {
                scheme: 'socks5',
                host: socksHost,
                port: parseInt(socksPort)
            },
            bypassList: bypasslist
        }
    };

    chrome.proxy.settings.set({ value: config, scope: 'regular' }, () => {
        console.log("SOCKS5 proxy set to:", socksHost + ":" + socksPort);
    });

    chrome.action.setIcon({ path: "images/on.png" });
}

// // Event listeners for saving settings and applying the proxy
// document.addEventListener('DOMContentLoaded', function() {
//     saveProxyHttpSettings("103.177.108.235", "6789", "xwci1j2w", "xWcI1j2w");
//     httpProxy();
// });
