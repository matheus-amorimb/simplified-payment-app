```
  __  __       _   _                      ____             
 |  \/  | __ _| |_| |__   ___ _   _ ___  |  _ \ __ _ _   _ 
 | |\/| |/ _` | __| '_ \ / _ \ | | / __| | |_) / _` | | | |
 | |  | | (_| | |_| | | |  __/ |_| \__ \ |  __/ (_| | |_| |
 |_|  |_|\__,_|\__|_| |_|\___|\__,_|___/ |_|   \__,_|\__, |
                                                     |___/ 
```
# Simplified Payment App

## Overview
Based on the Backend Challenge By PicPay, the Simplified Payment App is a web application designed to handle transactions and wallets for users.

## Project Structure

1. **SimplifiedPaymentApp**: This contains the backend logic, including controllers, services, repositories, and database context.
2. **EmailService**: This is a separate service responsible for sending email notifications related to transactions and wallet operations.

### Controllers

#### AuthController
Handles authentication-related actions such as user registration, login, role creation, and role assignment. Authentication policies are enforced to restrict certain actions to specific roles (e.g., only administrators can create roles).

#### TransactionController
Manages transactions, including retrieving transaction history for users and administrators and creating new transactions. Authorization is applied to ensure that users can only view and create transactions for their own wallets, while administrators have access to all transactions.

### WalletController
Manages wallets, including retrieving all wallets (admin-only) and creating new wallets (admin-only). Similar to the TransactionController, authorization is enforced to prevent users from accessing wallets that don't belong to them.

### Technologies Used
- **Backend**: C#, ASP.NET Core
- **Database**: Entity Framework Core with PostgreSQL
- **Authentication**: JWT (JSON Web Tokens)
- **Messaging**: RabbitMQ
- **Documentation**: Swagger UI
- **Email**: MailKit

## Using Repositories
Repositories are used to handle database actions in the project. They abstract away the details of data access and provide a clean interface for interacting with the database. Each repository corresponds to a specific entity in the database (e.g., UserRepository, WalletRepository) and contains methods for common CRUD operations.

## Setup Instructions
1. Clone the repository to your local machine.
2. Ensure you have the following prerequisites installed:
   - .NET SDK
   - PostgreSQL
   - RabbitMQ
3. Navigate to the `SimplifiedPaymentApp` directory and update the database connection string in `appsettings.json`.
4. Run the following commands to apply migrations and seed the database:
   ```bash
   dotnet ef database update
   ```
5. Run the following command to start the backend server:
   ```bash
   dotnet run
   ```
