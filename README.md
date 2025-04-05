# 🎭 Playwright .NET Automation Framework

[![.NET Version](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)]()
[![Playwright](https://img.shields.io/badge/Playwright-C%23-blueviolet)](https://playwright.dev/dotnet)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

A modern, fast, and reliable **end-to-end UI test automation framework** built with  
🟣 **Playwright**, 💙 **.NET 8**, and ✅ **NUnit** — designed to test modern web applications across all major browsers.

---

## ✨ Features

✅ Cross-browser support: **Chromium**, **Edge**, **Firefox**, **WebKit**  
📸 Tracing & Screenshots for failed tests  
🎯 Supports **Headless** & **Headed** modes  
🧪 **Playwright Inspector (PWDEBUG)** support  
📈 Allure & Extent Reports integration  
⚙️ CI/CD ready (Azure Pipelines or GitHub Actions)  
🧩 Modular base class with tracing, reporting, and cleanup

---

## ⚙️ Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (for Allure CLI)
- [Allure CLI](https://docs.qameta.io/allure/#_installing_a_commandline)

Install Playwright browsers after restoring packages:
```bash
pwsh ./bin/Debug/net8.0/playwright.ps1 install


```bash
dotnet restore
use:- dotnet build
use:- dotnet test
use:- PWDEBUG=1 dotnet test
use:- HEADED=1 dotnet test
use:- dotnet test -- Playwright.BrowserName=chromium
use:- dotnet test -- Playwright.BrowserName=chromium Playwright.LaunchOptions.Channel=chromium
use:- dotnet test -- Playwright.BrowserName=chromium Playwright.LaunchOptions.Channel=msedge
use:- dotnet test -- Playwright.BrowserName=chromium Playwright.LaunchOptions.Headless=false Playwright.LaunchOptions.Channel=msedge
use:- allure generate --clean bin/Debug/net8.0/allure-results -o allure-report
use:- allure open allure-report
use:- allure serve bin/Debug/net8.0/allure-results


PlaywrightCSharpFramework/
│
├── PlaywrightTests/                # Main test project
│   ├── testComponents/            # Base classes, hooks
│   ├── ExampleTest.cs             # Sample tests
│   └── allureConfig.json          # Allure configuration
│
├── .azure-pipelines.yml           # CI/CD pipeline config
└── README.md                      # You are here 😄
