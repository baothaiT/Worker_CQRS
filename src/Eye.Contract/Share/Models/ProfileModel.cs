using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eye.Contract.Share.Enum;
using OpenQA.Selenium;

namespace Eye.Contract.Share.Models
{
    public class ProfileModel
    {
        public string? Id { get; set; }
        public required string Name { get; set; }
        public int screenHeith { get; set; }
        public int screenWidth { get; set; }
        public int xPosition { get; set; }
        public int yPosition { get; set; }
        public IWebDriver? webDriver { get; set; } = null;
        public StatusProfileEnum statusProfileEnum { get; set; }
    } 
}
