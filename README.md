# Share Post Bot Telegram

[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://opensource.org/licenses/MIT)

**Overview**

This is a post-sharing bot for Telegram, where users can post content that is then reviewed by a few volunteer users. Once approved, the posts are sent to all users. The bot is designed using C#, .Net, and an SQL database.

**Features**

- **User Posting**: Users can submit posts through the bot.
- **Volunteer Review**: Submitted posts are reviewed by volunteer users.
- **Approval System**: Approved posts are distributed to all users.
- **Admin Controls**: Admins can manage users, posts, and volunteers.
- **Database Integration**: Utilizes SQL for storing posts and user data.

**Technologies Used**

- **Programming Language**: C#
- **Framework**: .Net
- **Database**: SQL

**Setup Instructions**

**Prerequisites**

- .NET Framework
- SQL Server
- Telegram Bot API Token

**Installation**

1. **Clone the Repository**

   ```

   Copy code

   git clone <https://github.com/babak3548/BotTelegram.git>

   ```

1. **Navigate to Project Directory**

   ```

   Copy code

   cd BotTelegram

   ```

1. **Configure Database**
   1. Set up your SQL Server and create a database for the bot.
   1. Update the connection string in the config file.
1. **Run the Application**

   ```

   Copy code

   dotnet run

   ```

**Usage**

1. **Register the Bot with Telegram**
   1. Obtain a Bot API token from BotFather.
   1. Update the token in the config file.
1. **Start the Bot**
   1. Users can start interacting with the bot by sending messages.
   1. Volunteer users will review submitted posts.
   1. Approved posts will be sent to all users.

**Contributing**

Contributions are welcome! Please fork the repository and submit a pull request.

**License**

This project is licensed under the MIT License. See the LICENSE file for details.

**Acknowledgements**

- Telegram Bot API

-----

