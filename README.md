# Setup
- cd OnlineShop
- docker compose build
- docker compose up
- open http://localhost:5100

### **Domain Layer**

1. **Entities**: Represents the core business objects.
   - **EntityBase**: Base class for all entities, providing a unique identifier `Id`.
   - **Product**: Represents a product with properties such as `Name`, `Price`, `ImageUrl`, and `Description`.
   - **ShoppingCart**: Represents a user's shopping cart, containing a list of `CartItem`.
   - **CartItem**: Represents an item in a shopping cart, with `ProductId` and `Quantity`.

### **Application Layer**

1. **Common Interfaces**:
   - **IReadRepository<TEntity>**: Defines methods for read-only operations like fetching entities by ID or specification.
   - **IRepository<TEntity>**: Extends `IReadRepository` to include methods for write operations such as `Add`, `Update`, and `Delete`.
   - **IShoppingCartRepository**: Extends `IRepository` specifically for `ShoppingCart` operations, including fetching by `UserId`.
   - **ISpecification<TEntity>**: Represents a query specification for filtering and sorting entities.

2. **Mappings**:
   - **MappingProfile**: Configures mappings between domain entities and data transfer objects (DTOs) using AutoMapper.

3. **Queries and Handlers**:
   - **GetAllProductsQuery**: Request to retrieve all products.
   - **GetAllProductsQueryHandler**: Handles `GetAllProductsQuery`, using `IReadRepository` to fetch and map products.
   - **GetSingleProductQuery**: Request to retrieve a single product by ID.
   - **GetSingleProductQueryHandler**: Handles `GetSingleProductQuery`, validating and fetching a product by ID.
   - **ProductModel**: DTO for transferring product data.
   - **ProductsSortedAscByNameSpec**: Specification for sorting products by name.

4. **Shopping Cart**:
   - **AddToCartQuery**: Request to add a product to a cart.
   - **AddToCartQueryHandler**: Manages adding products to the cart, including updating quantities.
   - **GetShoppingCartQuery**: Request to retrieve a user's shopping cart.
   - **GetShoppingCartQueryHandler**: Handles fetching the shopping cart, calculating discounts.
   - **RemoveFromCartQuery**: Request to remove an item from the cart.
   - **RemoveFromCartQueryHandler**: Manages removing items from the cart.
   - **CartItemModel**: DTO for items in the shopping cart.
   - **ShoppingCartModel**: DTO for the shopping cart, including items and discount details.

5. **Services**:
   - **DiscountCalculationService**: Calculates applicable discounts for a shopping cart, such as buy-one-get-one or spend thresholds.

6. **Validations**:
   - **AddToCartQueryValidator**: Validates `AddToCartQuery` ensuring proper user ID, product ID, and quantity.
   - **GetShoppingCartQueryValidator**: Validates `GetShoppingCartQuery`.
   - **GetSingleProductQueryValidator**: Validates `GetSingleProductQuery`.
   - **RemoveFromCartQueryValidator**: Validates `RemoveFromCartQuery`.

7. **ConfigureServices**: Registers application services with the dependency injection container, including MediatR, AutoMapper, and validators.

### **Infrastructure Layer**

1. **Persistence - MongoDB**:
   - **MongoDbContext**: Manages MongoDB connection and database access.
   - **MongoDbOptions**: Configurations for connecting to MongoDB.
   - **MongoDbReadRepository<TEntity>**: Implements read-only repository operations using MongoDB.
   - **MongoDbRepository<TEntity>**: Extends `MongoDbReadRepository` to include write operations.
   - **MongoDbShoppingCartRepository**: Specific repository for `ShoppingCart` entity operations.
   - **MongoDbInitializer**: Seeds the MongoDB database with initial data.
   - **DbInitializerExtension**: Middleware extension for initializing the MongoDB database during application startup.

2. **ConfigureServices**: Registers infrastructure services with the DI container, including MongoDB context and repositories.

### **Web API Layer**

1. **Controllers**:
   - **ProductsController**: Manages product-related API operations.
   - **ShoppingCartController**: Manages shopping cart-related API operations, including adding, retrieving, and removing items.

2. **Middlewares**:
   - **ExceptionHandlingMiddleware**: Custom middleware for handling exceptions and returning standardized error responses.
   - **ErrorDetails**: Represents error details returned by the middleware.

3. **Startup Configuration**:
   - **ConfigureServices**: Registers services and middleware for the Web API layer, including controllers, Swagger, and CORS policy.
   - **Program.cs**: Main entry point for the application, configuring the HTTP request pipeline and starting the application.

### **React Frontend**

1. **Components**:
   - **CartCount**: Displays the number of items in the cart, using Redux state.
   - **ShoppingCart**: Manages the shopping cart UI, including item manipulation and discount display.
   - **Layout**: Main layout component, includes navigation and routing structure.
   - **NavMenu**: Navigation menu with links and cart item count.
   - **ProductDetails**: Displays detailed information about a product, with an option to add it to the cart.
   - **ProductList**: Lists all products, each linking to the detailed view.
   - **ProductListItem**: Represents a single product in the list, including add-to-cart functionality.

2. **API**:
   - **ProductsApi**: Functions for interacting with the products API, including fetching all products and a single product.

3. **Redux Store**:
   - **cartActions**: Defines actions for managing the shopping cart state, including adding, removing, and updating items.
   - **cartReducer**: Handles state changes in the shopping cart based on actions dispatched.
   - **store**: Configures the Redux store, integrating reducers and middleware.

4. **Routing**:
   - **App**: Root component configuring routes and rendering the main layout.
   - **AppRoutes**: Defines the application's routes, linking paths to components.

5. **Entry Point**:
   - **index.js**: Sets up the React application, integrating Redux and React Router, and renders the root component.

### **Unit Tests**

1. **GetAllProductsQueryHandler_Handle**: Tests for `GetAllProductsQueryHandler`, ensuring proper handling of different scenarios.
2. **GetSingleProductQueryHandler_Handle**: Tests for `GetSingleProductQueryHandler`, covering valid and invalid requests.
3. **AddToCartQueryHandler_Handle**: Tests for `AddToCartQueryHandler`, validating cart manipulation logic.
4. **GetShoppingCartQueryHandler_Handle**: Tests for `GetShoppingCartQueryHandler`, checking cart retrieval and validation.
5. **RemoveFromCartQueryHandler_Handle**: Tests for `RemoveFromCartQueryHandler`, ensuring correct item removal from the cart.

### **Deployment**

1. **Docker**:
   - **Dockerfile**: Defines the steps for building Docker images for the backend and frontend.
   - **docker-compose.yml**: Configures Docker services for the application, including the web API, MongoDB, and the frontend UI.
