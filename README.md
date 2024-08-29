# VbWorkerServicePinvokeLauncher

## Overview

**VbWorkerServicePinvokeLauncher** is a .NET 8.0 Worker Service written in Visual Basic. This service is designed to launch and manage processes under specific user accounts, such as the SYSTEM account, by duplicating process tokens. It utilizes P/Invoke to interact with Windows APIs, enabling the service to start processes with elevated privileges or different user contexts.

## Features

- Launch processes under specific user accounts.
- Duplicate process tokens for elevated privilege management.
- Use P/Invoke to interact with Windows APIs.
- Configuration via `appsettings.json`.

## Installation

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- A compatible development environment (e.g., Visual Studio 2022 or later).

### Steps to Install

1. **Clone the Repository**

   ```bash
   git clone https://github.com/1d3nt/VbWorkerServicePinvokeLauncher.git

## Installing and Starting the Service

### Install the Service

```bash
sc create VbWorkerService binPath= "Path\VbWorkerServicePinvokeLauncher.exe"
```

### Start the service

```bash 
sc start VbWorkerService
```

This setup allows users to adjust the path to their specific needs and handles the installation and starting of the service.







