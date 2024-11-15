### Usage
1. Navigate in the terminal to the folder of OnlineStoreApi
2. In the terminal enter the command `docker compose up`
3. please start the program and navigate to **http://localhost:5252/swagger/index.html** to see all the features for this application 
4. In swagger please make a new user by doing a POST on /api/Users (and please copy this id)
5. In swagger also create a new product by doing a POST on /api/Products (and please copy this id)
6. You are now able to create a new order by doing a POST on /api/Orders. Paste the id's of the user and the product(s). Hit enter and the result will be a new order (without a shipping date)
7. in the terminal you will now see that both HandleOrdersTimeTriggerFunction and OrderQueueTrigger both have done their jobs. 



# Solution Overview
This solution consists of 3 main projects:

### OnlineStoreApi
This is the web application where all the CRUD functionalities happen.

### HandleOrdersTimeTrigger
Every night at 00:00, this Azure Function fires a timer trigger and retrieves all orders (asynchronously). For each order with a `null` shipping date, it places a message on the queue.

### OrderQueueTrigger
Handles the queue messages and updates the shipping order to the correct shipping date.

## Rubrics

### Scalability of Data Services
Although the requirements specify a "traditional SQL database," I chose **NoSQL (MongoDB)** due to its scalability, flexibility, and performance benefits. MongoDB’s ability to horizontally scale makes it ideal for handling high traffic loads during peak hours. Additionally, its flexible schema allows for quick adjustments to data models (e.g., orders, reviews) without requiring rigid restructuring. Optimized read/write operations and support for eventual consistency make it well-suited for high-frequency order processing and managing semi-structured data, such as product reviews, which align with the dynamic nature of the application.

### N-Tier Application
Within the OnlineStoreApi, the flow follows a structured path:
- **Controller** ➔ **Interface Service** ➔ **Service** ➔ **Interface Repository** ➔ **Repository**

Two Azure Functions are used:
1. **HandleOrdersTimeTriggerFunction** (Timer Trigger)
2. **OrderQueueTrigger** (Queue Trigger)

### Loosely Coupled Services
- I use Dependency Injection in both **OrderQueueTrigger** and **HandleOrdersTimeTriggerFunction** to register shared services like `QueueClient`, `OrderService`, and `UpdateShippingService`, allowing centralized, testable, and reusable configurations across both functions.

### Command Query Responsibility Segregation (CQRS)
- The web application (OnlineStoreApi) architecture follows CQRS by ensuring a clear separation between read and write operations. Write operations (commands) are handled using Request DTOs (e.g., `CreateOrderRequest`), while read operations (queries) are represented by Response DTOs (e.g., `CreateOrderResponse`).
