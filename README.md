# 🛒 Shopli - Modern E-Commerce Marketplace

A full-featured, cross-platform e-commerce application built with **ASP.NET Core** and modern web technologies. Shopli provides a complete shopping experience with product categorization, wishlist management, user authentication, and a responsive design.

## 🌟 Key Features

### 🎯 **Core E-Commerce Functionality**
- **Product Catalog Management** - Browse and search products with detailed information
- **Category System** - Organized product categories with dedicated category pages
- **Shopping Cart** - Add, remove, and manage items in cart
- **User Authentication** - Secure registration, login, and user management
- **Wishlist** - Save favorite products with AJAX-powered interactions
- **Responsive Design** - Mobile-first approach for all devices

### 🔧 **Technical Highlights**
- **Cross-Platform Database** - SQLite for universal compatibility
- **Clean Architecture** - Separation of concerns with Domain, Repository, Service layers
- **Repository Pattern** - Async/await support with Entity Framework Core
- **RESTful APIs** - Well-structured API endpoints
- **Modern UI/UX** - Bootstrap 5 with custom styling and animations

## 🏗️ Architecture Overview

```
EShop.Web/              # MVC Web Application
├── Controllers/        # API and MVC Controllers
├── Views/             # Razor Views and Layouts
└── wwwroot/           # Static files (CSS, JS, Images)

EShop.Service/          # Business Logic Layer
├── Interface/         # Service Contracts
└── Implementation/    # Service Implementations

EShop.Repository/       # Data Access Layer
├── Interface/         # Repository Contracts
├── Implementation/    # Repository Implementations
└── Migrations/        # Database Migrations

EShop.Domain/          # Domain Models
├── Domain Models/     # Core Business Entities
├── DTO/              # Data Transfer Objects
└── Identity Models/   # User Authentication Models
```

## 📊 Database Schema

### Core Entities
- **Products** - Product catalog with categories, pricing, and inventory
- **Categories** - Product categorization system
- **Users** - Customer accounts and authentication
- **Shopping Cart** - User cart management
- **Wishlist** - User favorite products
- **Orders** - Purchase history and order management
- **Reviews** - Product ratings and feedback

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code
- SQLite (included)

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd Shopli-Marketplace
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Update database**
   ```bash
   dotnet ef database update --project EShop.Repository --startup-project EShop.Web
   ```

4. **Run the application**
   ```bash
   dotnet run --project EShop.Web
   ```

5. **Access the application**
   - Navigate to `http://localhost:5001`
   - Register a new account or continue as guest

## 🎨 Features Showcase

### 🏠 **Dynamic Homepage**
- **Full-width hero banner** with contextual call-to-action buttons
- **Smart authentication detection** - different buttons for logged-in vs guest users
- **Featured categories** section with interactive cards
- **Responsive design** that adapts to all screen sizes

### 📱 **Category Management**
- **Automated categorization** - Products intelligently assigned to categories
- **Category browsing** - Dedicated pages for each product category
- **Empty state handling** - Graceful display when categories have no products
- **Category seeding** - Pre-populated categories (Electronics, Books, Clothing, Sports, Home & Garden)

### ❤️ **Wishlist System**
- **AJAX interactions** - Add/remove items without page refresh
- **Visual feedback** - Toast notifications for user actions
- **Persistent storage** - Wishlist items saved to database
- **User-specific** - Each user has their own wishlist

### 🛒 **Shopping Cart**
- **Real-time updates** - Instant cart updates
- **Quantity management** - Adjust item quantities
- **Cart persistence** - Cart items saved across sessions
- **Checkout flow** - Streamlined purchase process

### 🔐 **Authentication & Authorization**
- **ASP.NET Core Identity** integration
- **Secure user registration** and login
- **Role-based access control**
- **Password security** with hashing and validation

## 🔧 Technical Implementation

### 🗄️ **Database Migration from SQL Server to SQLite**
```csharp
// Before: SQL Server LocalDB (Windows-only)
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EShop;Trusted_Connection=true"
}

// After: SQLite (Cross-platform)
"ConnectionStrings": {
  "DefaultConnection": "Data Source=eshop.db"
}
```

### 🎯 **Service Layer Implementation**
```csharp
public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<List<Product>> GetProductsByCategoryAsync(Guid categoryId);
    // ... other methods
}

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Product> _productRepository;
    
    // Efficient async implementation with EF Core
}
```

### 🔄 **AJAX Wishlist Integration**
```javascript
// Real-time wishlist updates
$('.wishlist-btn').click(function() {
    $.post('/Wishlist/Toggle', { productId: productId })
     .done(function(response) {
         showToast(response.message, response.success ? 'success' : 'error');
         updateWishlistUI(response);
     });
});
```

## 📈 Development Process & Achievements

### 🔄 **Migration & Modernization**
1. **Database Migration** - Successfully migrated from SQL Server to SQLite for cross-platform compatibility
2. **Architecture Refactoring** - Enhanced repository pattern with async/await support
3. **UI Modernization** - Upgraded to Bootstrap 5 with custom styling
4. **Responsive Design** - Mobile-first approach implementation

### 🆕 **Feature Development**
1. **Category System** - Built complete categorization with automated product assignment
2. **Wishlist Functionality** - Implemented user-specific wishlists with AJAX interactions
3. **Enhanced Product Models** - Added reviews, ratings, and product images support
4. **API Development** - Created RESTful endpoints for all major operations

### 🐛 **Problem Solving**
1. **NullReferenceException Fixes** - Resolved shopping cart service issues
2. **EF Core Query Optimization** - Fixed relationship loading and includes
3. **UI/UX Issues** - Resolved full-width banner constraints in layout
4. **Cross-platform Compatibility** - Ensured application runs on all operating systems

## 🎯 Key Accomplishments

### 💼 **Business Value**
- **Modern E-commerce Solution** - Complete marketplace functionality
- **Cross-platform Compatibility** - Runs on Windows, macOS, and Linux
- **Scalable Architecture** - Clean separation of concerns for easy maintenance
- **User Experience Focus** - Responsive design and smooth interactions

### 🔧 **Technical Excellence**
- **Clean Code Principles** - Well-structured, maintainable codebase
- **Modern Technology Stack** - ASP.NET Core 8, Entity Framework Core, Bootstrap 5
- **Database Design** - Normalized schema with proper relationships
- **Security Implementation** - ASP.NET Core Identity with secure authentication

### 🚀 **Implementation Highlights**
1. **Successfully migrated** legacy database to modern SQLite solution
2. **Implemented complete category system** with automated product assignment
3. **Built interactive wishlist** with real-time AJAX updates
4. **Created responsive, modern UI** with full-width hero sections
5. **Resolved critical bugs** and performance issues

### 📊 **Metrics & Results**
- **100% Cross-platform compatibility** - Works on all major operating systems
- **Zero database dependencies** - SQLite embedded, no server required
- **Mobile-responsive design** - Optimized for all screen sizes
- **Complete feature set** - All core e-commerce functionality implemented

## 🔮 Future Enhancements

### 🌟 **Planned Features**
- **Payment Integration** - Stripe/PayPal integration
- **Product Reviews** - Customer review and rating system
- **Search & Filtering** - Advanced product search capabilities
- **Admin Dashboard** - Complete admin panel for management
- **Email Notifications** - Order confirmations and updates
- **Multi-language Support** - Internationalization

### 🔧 **Technical Improvements**
- **Caching Implementation** - Redis for improved performance
- **API Documentation** - Swagger/OpenAPI integration
- **Unit Testing** - Comprehensive test coverage
- **Docker Support** - Containerization for deployment
- **CI/CD Pipeline** - Automated deployment workflows

---

## 🏆 Project Highlights

> "A modern, full-featured e-commerce solution showcasing clean architecture, cross-platform compatibility, and excellent user experience design."

### Key Differentiators:
✅ **Cross-platform SQLite database**  
✅ **Real-time AJAX interactions**  
✅ **Responsive, mobile-first design**  
✅ **Clean architecture principles**  
✅ **Complete e-commerce feature set**  

---

*Built with ❤️ using ASP.NET Core and modern web technologies*

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
