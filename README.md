🛒 Shopli – E-Commerce Demo Application
Shopli is a demo ASP.NET Core MVC web application built for educational purposes. It implements the Onion Architecture design pattern to promote clean separation of concerns and follows modern software engineering practices such as external API integration, layered services, and cloud deployment.

⚠️ Note: This is not a production-level application. It is built to demonstrate architectural patterns, third-party integrations (like Stripe), and publishing to Azure.

✅ Features
🧅 Onion Architecture
The solution is structured into:

Domain – Core business models and interfaces

Repository – Data access layer (Entity Framework Core)

Service – Business logic layer

Web – MVC front-end and controllers

📦 5 Domain Models
Includes full CRUD operations for at least five models (e.g., Product, Order, ShoppingCart, etc.).

🔁 External API Integration
Fetches product data from FakeStore API, transforms it, and displays it within the UI.

💳 Stripe Payment Integration
Real-world integration with Stripe for online payments (test mode).

☁️ Deployed to Azure
The app is published and running live on Azure:
👉 Live Demo

❌ What is Not Implemented (Yet)
🚫 User Roles and Authorization
Role-based access control (e.g., admin vs. user) is not implemented in this version, but the architecture supports easy extension.

🔐 Stripe Keys in Production
For the purpose of demonstration, Stripe keys are managed via environment variables in Azure App Service.

🧪 No Unit or Integration Tests
This version focuses on structure and deployment; testing is not included.
