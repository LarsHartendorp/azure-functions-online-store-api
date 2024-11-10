This solution consists of 3 main projects:
### OnlineStoreApi
This is the web application where all the CRUD functionalities happen. 

### HandleOrdersTimeTrigger
Every night at 00:00 this azure function fires a timer trigger and retrieves all the orders (async) and puts a message
on the queue foreach order where the shipping date is null. 

### OrderQueueTrigger
Handles the queue messages and updates the shipping order to the correct shipping date. 

## Rubrics
**Scalability of Data Services**<br>
Although the requirements specify a "traditional SQL database,"
I chose **NoSQL (MongoDB)** due to its scalability, flexibility, and performance benefits.
MongoDB's ability to horizontally scale makes it ideal for handling high traffic loads during peak hours,
while its flexible schema allows for quick adjustments to data models (e.g., orders, reviews) without rigid restructuring.
Additionally, its optimized read/write operations and support for eventual consistency are well-suited for
high-frequency order processing and managing semi-structured data, such as product reviews,
making it a better fit for the dynamic nature of the application.

**Resource Allocation**<br>