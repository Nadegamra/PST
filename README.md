# Product development project (Kaunas University of Technology student group project)
### Webpage idea:
- A platform for lending/borrowing various gaming consoles
### Main functionalities: 
- Lend consoles (Lenders)
- Borrow consoles (Borrowing companies)
- Manage available console categories (Admin)
- Manage registered consoles and borrowings (Admin)
- Create chats with borrowers/lenders (Admin)
- Converse in created chats + share files (All user roles)
### Other features:
- I18n (Available languages: Lithuanian and English)
- Multiple themes (Light and Dark)
- Responsive design  
# Technology stack:
- .NET7 + EF Core
- React.js + TypeScript
- MySql
# Requirements
- Having Docker installed
# Launch instructions
- Configure app (dev.env):
  - Specify SMTP account credentials (Email functionalities)
  - Specify Cloudinary credentials (File Upload functionality) and update frontend/src/models/Image.tsx imagePathToUrl() function (image display)
### `dev.env` file example
```
  Smtp__Username='sender@example.com'
  Smtp__Password='abcdefghijklmnop'
  Smtp__TestEmail='receiver@example.com'
  ImageStorage__Cloud='abcdefghi'
  ImageStorage__ApiKey='123456789101112'
  ImageStorage__ApiSecret='AB-CD1-23abcdefABCDEF456789'
```
- Generate local database:
  Perform `dotnet ef database update` command from a console/terminal window in `Backend` directory 
- Perform `docker compose up` command from the root directory



### Default user accounts
- Admin: Username='admin@admin.com' Password='Password123!'
- Customer: Username='customer@example.com' Password='Password123!'
- Company: Username='company@example.com' Password='Password123!'
