## Given more time I would:

### Single responsibility
The Web.API should be responsible for uploading of the file, and should hand off the processing to another system. This separates the concerns of uploading and processing, protects against transient failures and offers a durable store for files before validation.

### Eventual consistency / processing / scalability
The Web.API would use the Outbox pattern to both persist and raise an event in the outbox, implemented by MassTransit and delivered using RabbitMQ.

The consuming Processor application would process messages idempotently, to pick up processing jobs from unprocessed CSV files.

### Containerisation
Both of the above would be containerised to allow for platform agnostic deployments.

### Hexagonal, or ports and adapters, architecture
Pipeline behaviours also address cross-cutting concerns, such as logging, auditing, exception handling, validation.

### Audit, Monitoring, Logging, Healthz
From a DevOps perspective, there is a lot that can and should be done.

### Construction
It might be worth considering whether using a Factory or static constructor would provide any value to constructing the domain models.

### Automapper
To make it simplier to pass mapped domain models back to the consumers.

### Use DI and the repository pattern
Rather than integate the context directly from the Application.

### Flesh out the Response pattern
Include static constructors for both success and failed reponses.

### Use better exceptions
The exceptions thrown in the domain that violate invariants should be DomainInvariantExceptions

### Look at API.Records
I'm sure there's a better solution to this. The model should be losely coupled to the CSV implementation.

### Mediatr pipeline
Add at some pipeline behaviour like logging at least.

### Response parsing
Find a better way to aggregate the responses generated from the application.

### CSVReader on handling errors
Spend far too much time trying to figure parsing errors and eventually hacked a nasty work-around in. Needs revisiting.

### Customer account pulls all meter readings
Could choose to use lazy loading instead, careful here.

### HTTP Middleware
For better logging, exception handling and performance monitoring. Not great dropping whole exceptions to the frontend.

### More testing
Add tests around CustomerAccount and integration tests.

### CSV parsing
Could be refactored out into a more suitable utilies class. Not great left clogging up the controller.

### Batch processing
Running tasks async to speed up processing rather than sequentially would be better.

### Inject configuration
Rather than hardcoding checks, could be made configurable.

### Add queries
To be able to retreive customer's meter readings.

