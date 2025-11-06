# Order Status Tracker

A full-stack order tracking application built with C# Web API backend and Angular frontend.

## Tech Stack

**Backend:**
- .NET 9 Web API
- C#
- Swagger/OpenAPI documentation

**Frontend:**
- Angular 20
- TypeScript
- RxJS

## Features

- Search orders by order number
- View detailed order information
- Timeline visualization of order status history
- Simulate order status progression
- Error handling for invalid orders
- Loading states and success notifications

## Project Structure
```
order-status-tracker/
├── OrderTrackerAPI/         # C# Web API backend
│   ├── Controllers/         # API endpoints
│   ├── Models/              # Data models
│   ├── Services/            # Business logic
│   └── Program.cs           # Application entry point
└── order-tracker-ui/        # Angular frontend
    └── src/app/
        ├── order-detail/    # Detail component
        ├── order-search/    # Search component
        └── services/        # API service layer
```

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)

## Setup Instructions

### Backend Setup

1. Navigate to the API directory:
```bash
cd OrderTrackerAPI
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run the API:
```bash
dotnet run
```

The API will start on `http://localhost:5075`

Swagger documentation available at: `http://localhost:5075/swagger`

### Frontend Setup

1. Navigate to the UI directory:
```bash
cd order-tracker-ui
```

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
ng serve
```

The application will be available at `http://localhost:4200`

## Sample Order Numbers

Try these order numbers to see different states:
- `ORD-10001` - Delivered (full history)
- `ORD-10002` - Shipped (can advance status)
- `ORD-10003` - Processing (can advance status)
- `ORD-10004` - Pending (can advance status)

## API Endpoints

### GET /api/orders/{orderId}
Returns order summary including current status, total amount, and order date.

### GET /api/orders/{orderId}/history
Returns complete status history for an order with timestamps and notes.

### POST /api/orders/{orderId}/advance-status
Simulates order progression by advancing to the next status.

## Architecture Decisions

### Backend
- **Service Layer Pattern**: Separated business logic (OrderService) from HTTP concerns (OrdersController) for better testability and maintainability
- **Dependency Injection**: OrderService is automatically provided to the controller by .NET, making the code more testable and flexible
- **Repository Pattern Ready**: Data access is abstracted in OrderService
- **Entity Framework Modeling**: Models structured as they would be for EF Core (primary keys, foreign keys, navigation properties)

### Frontend
- **Component Architecture**: Clear separation between search and detail components
- **Service Layer**: API calls isolated in OrderService for reusability
- **RxJS Observables**: Used forkJoin to call multiple endpoints simultaneously
- **Modern Angular**: Utilized new control flow syntax (@if, @for) instead of deprecated directives

## Security Considerations

**Current Implementation:**
- Input validation on order ID format
- Proper HTTP status codes (404, 400, 200)
- CORS configuration for local development

**Production Additions Needed:**
- Authentication & Authorization (JWT with [Authorize] attributes)
- Validate users can only access their own orders (e.g. order.CustomerId == User.Identity.UserId)
- Environment-based configuration (API keys, connection strings)
- Request rate limiting
- Structured logging for security events

**Note:** The TODO comments in the controller indicate where authentication checks would be added in a production environment.

## Testing

Automated tests were not implemented. In a production environment, I would add:
- Backend: Unit tests for OrderService business logic and controller endpoints
- Frontend: Component tests for user interactions and service API calls
- End-to-end tests for the complete order lookup flow

## Future Enhancements

- Real database integration (SQL Server with Entity Framework Core)
- User authentication and authorization
- Order creation and modification
- Advanced filtering and search capabilities
- Pagination for order history
- Export order details to PDF
