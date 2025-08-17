✅ MVC Incremental Requirements Part 4 – API Extension
Add a Web API Layer to Your Project

Expose API Endpoints for your existing Models (at least 3 models).

Example: api/products, api/orders, api/customers.

Endpoints should support:

GET → GetAll + GetById

POST → Create

PUT → Update

DELETE → Delete

Return JSON Results (use ActionResult<T> and proper status codes).

Use Repository Pattern + Dependency Injection so your API and MVC controllers share the same business logic.

Add API Versioning (at least v1 routes).

Secure the API:

Apply Authentication (use the Identity system you added in Part 3).

Protect certain endpoints so only Admins can create/update/delete.

Allow Users to fetch data but not modify it.

Bonus Ideas:

Add Swagger (OpenAPI) documentation to visualize and test your API.

Implement DTOs for API responses instead of sending EF Core entities directly.

Add CORS policy to allow requests from other clients.