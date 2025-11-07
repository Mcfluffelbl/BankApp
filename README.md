# BankApp
A simple application to help you keep track of your finances.

## Features
- [Overveiw](#overview)
- [Features](#features)
- [Getting Started](#getting-started)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [Project Structure](#project-structure)
- [License](#license)
- [Screenshots](#screenshots)

## Overview
BankApp is a lightweight application designed to help users manage their finances effectively.
It allows users to create and manage accounts, track transactions, monitor withdrawals, add intrest yearly, and view financial summaries — all stored locally.

## Features
- Account Management: Create, update, and delete bank accounts.
- Transaction Tracking: Record and categorize transactions.
- Withdrawal Tracking: Record and categorize withdrawel for kepping check on finances.
- Account Summary: View balances and recent transactions.
- Money Transfer: Transfer money between accounts.
- Money Transfer Tracking: Track money transfers between accounts.
- Accounts saved in users local storage.
- Real-time updates: Keeps track of dates and times of transactions and accounts.

## Getting Started
Follow these steps to get started with BankApp.

### Prerequisites
A modern web browser (Chrome, Edge, Firefox, etc.)

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/Mcfluffelbl/BankApp.git
2. Restore dependencies
   dotnet restore
3. Run the app
   dotnet run
4. Open in your browser
   http://localhost:5000

### Usage

1. Log in to the application using the code "1234".
2. Create a new account by navigating to the "Accounts" page and clicking "Add Account".
3. Record transactions by selecting an account and clicking "Add Transaction".
4. Veiw account and recent transactions on the "History" page.

## Configuration
BankApp does not require any additional configuration. All data is stored locally in the user's browser.

## Project Structure

- Models/: Contains the data models for accounts and transactions.
- Services/: Contains services for managing accounts and transactions.
- App/: Root component.
- Pages/: Contains the Razor components for different pages in the application.
- wwwroot/: Contains static files such as CSS, C# , JS and icons.
- Program.cs: Configures the application and services.
- Interfaces/: Contains interface definitions for services.
- Readme.md: This file.

## License
This project is not licensed.
You may modify or use it at your own discretion.

## Screenshots
Screenshots are available in the wwwroot/images folder, named sequentially as ReadMe-Picture-Name-1, ReadMe-Picture-Name-2, etc.