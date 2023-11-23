# MandelsBankenConsole

## Description
Mandelsbanken is a simple console application developed as a school project to apply knowledge in C#, SQL, Entity Framework Core, and Git in a group setting.

## Installation
To set up and run the Mandelsbanken Console Application:

1. Clone this repository to Visual Studio Code.
2. Create a new SQL Server database with a name of your choice.
3. Obtain the connection string from the "Server Explorer" in Visual Studio Code and add it to "BankenContext.cs" in the "Data" folder.
4. Open the Package Manager Console and run "Update-Database".
5. Execute the SQL code for currencies in SQL Server Management Studio.

## Usage
When you first open the program, you can either log in with a social security number added to your database or log in as an admin with the username "admin" and pin code "1234".

### Admin Functionalities
- Create a new user by providing first and last name and social security number.
- Display all users and the total number of users.
- Exit the application.

### User Functionalities
- View accounts and balances.
- Transfer between accounts, with currency exchange if needed.
- Withdraw money, considering currency exchange and PIN verification.
- Deposit money, specifying amount and currency, with currency exchange if needed.
- Open a new savings account with a customizable name, initial deposit, and currency.
- Log out and return to first page.

For transfers, withdrawals, and deposits, successful transactions are logged in the database's transaction table.

### Navigation
- Use arrow keys to navigate menus.
- Press Enter to select an option.

### Validators
Different validators ensure proper user input, and error messages are displayed in red, while successful operations are shown in green.

### API Integration
The currency exchange utilizes an API to fetch the current exchange rate between the selected currencies before performing the calculation.