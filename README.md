# 🛒 Shopli – E-Commerce Demo Application

**Shopli** is a demo ASP.NET Core MVC web application built for educational purposes.  
It follows the **Onion Architecture** to ensure clean separation of concerns and modern best practices such as external API integration, service abstraction, and cloud deployment.

> ⚠️ **Note**: This is *not* a production-level application. It is built to demonstrate architectural patterns, third-party integration (Stripe), and Azure deployment.

---

## ✅ Features

### 🧅 Onion Architecture
The solution is structured in logical layers:
- **Domain** – Core business logic and model definitions  
- **Repository** – Data access using Entity Framework Core  
- **Service** – Business logic and service abstraction  
- **Web** – MVC front-end and controller logic  

### 📦 CRUD Operations
Implements **full CRUD** for **5 different models**, including:
- Products  
- Orders  
- Shopping Cart  
- Categories  
- Payments  

### 🔁 External API Integration
Uses the [FakeStore API](https://fakestoreapi.com/products) to import and display external product data in a transformed way.

### 💳 Stripe Payment Integration
Real-world integration with **Stripe** payment gateway to support online payments (test mode).

### ☁️ Azure Deployment
The app is deployed on Microsoft Azure:
🔗 [Live Demo](https://eshopweb20250606203405.azurewebsites.net/)

---

## ❌ What’s Not Included

- 🚫 **Role-based Authorization**  
  User roles (e.g., Admin/User) are not implemented in this version, but the architecture is ready for extension.

- 🧪 **Unit & Integration Tests**  
  Testing is not included, since the focus was on structure, services, and deployment.

- 🔐 **Production Secrets**  
  Stripe secret keys are managed via Azure environment variables.
