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



PlaywrightCSharpFramework/
│
├── PlaywrightTests/                # Main test project
│   ├── testComponents/            # Base classes, hooks
│   ├── ExampleTest.cs             # Sample tests
│   └── allureConfig.json          # Allure configuration
│
├── .azure-pipelines.yml           # CI/CD pipeline config
└── README.md                      # You are here 😄


## ⚙️ Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (for Allure CLI)
- [Allure CLI](https://docs.qameta.io/allure/#_installing_a_commandline)

# Setup
- [Guide](https://playwright.dev/dotnet/docs/intro)

# Install Playwright browsers after restoring packages:
```powershell
    pwsh ./bin/Debug/net8.0/playwright.ps1 install
```

# Bash commands
``` bash commands
    use:- dotnet restore
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
```

# Playwright commands
```powershell commands to record test (url is optional)
    use:- pwsh bin/Debug/net8.0/playwright.ps1 codegen demo.playwright.dev/todomvc
    use:- pwsh bin/Debug/netX/playwright.ps1 codegen --viewport-size="800,600" playwright.dev
    use:- pwsh bin/Debug/netX/playwright.ps1 codegen --device="iPhone 13" playwright.dev
```

# SSO test recording
    pwsh bin/Debug/netX/playwright.ps1 codegen github.com/microsoft/playwright --save-storage=auth.json
    pwsh bin/Debug/netX/playwright.ps1 codegen --load-storage=auth.json github.com/microsoft/playwright


# Multiple Page handle
```Code (https://playwright.dev/dotnet/docs/pages)

    // Get page after a specific action (e.g. clicking a link)
    var newPage = await context.RunAndWaitForPageAsync(async () =>
    {
        await page.GetByText("open new tab").ClickAsync();
    });
    // Interact with the new page normally
    await newPage.GetByRole(AriaRole.Button).ClickAsync();
    Console.WriteLine(await newPage.TitleAsync());
```
# 