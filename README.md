# Recipe Book 🍲📚

**Recipe Book** is a web platform that allows users to create, manage, and discover recipes

---

## 📌 Features

### Recipe Management:
- **Create Recipes**: 
  - Add recipes categorized by dish type (e.g., Appetizer, Main Course, Dessert).
  - Specify preparation time and cooking difficulty (Easy, Medium, Hard).
- **Discover Recipes**: Browse and search for recipes based on user preferences.

### AI Recipe Generator:
- **Generate Recipes with ChatGPT**: 
  - Let ChatGPT suggest a recipe based on selected ingredients or dish type.
  - Customize AI-generated recipes with additional steps or modifications.

### Account Services:
- **Google Login**: Simplified user authentication through Google accounts.
- **Message Service for Account Deletion**: Automatically delete user recipes when an account is deleted, ensuring data privacy.

---

## 🛠️ Technologies Used

- **Backend**: ASP.NET Core  
- **Database**: SQL Server  
- **Frontend**: React or Angular  
- **AI Integration**: OpenAI's ChatGPT API  
- **Authentication**: Google OAuth 2.0  
- **Messaging Service**: Azure Service Bus or RabbitMQ  

---

## 🚀 How to Use

### Creating a Recipe:
1. Login to the platform using your Google account.
2. Navigate to the **Create Recipe** section.
3. Enter details:
   - Dish Type (e.g., Appetizer, Main Course, Dessert).
   - Preparation Time (e.g., 30 minutes).
   - Difficulty Level (Easy, Medium, Hard).
4. Submit to save your recipe to your collection.

### Generating Recipes with AI:
1. Go to the **AI Recipe Generator**.
2. Select ingredients or a dish type.
3. Let ChatGPT generate a custom recipe for you.
4. Save or modify the recipe as needed.

### Managing Account:
- **Account Deletion**: 
  - When a user deletes their account, a messaging service ensures all their recipes are deleted automatically from the database.

