## Thought process

Given the simplicity of the problem space, using Domain Driven Design might be considered an anti-pattern here.

For sake of simpilicity, I will assume no bad actors or malicious payloads.

Likewise I will ignore authentication and authorisation for simplicity.

I will still ensure the project follows CQS patterns to ensure the API is decoupled from the Application.

~~The whole operation will be performed atomically. The file will either completely succeed to be processed, or completely fail with no consiquences.~~
This is not required, partial processing is desired.

The application will provide a list of aggregated errors for those records that fail processing and a reason.

I will use an in-memory database, transient to the application's lifetime.

File type and size validation will be done in the API to ensure we fail fast on easy to catch human errors.

CSV parsing validation will be performed in the Application layer to ensure no corrupt data enters the process.

Domain validation will be encapsulated in the domain model of a MeterReading.

Try to avoid primitive obsession where possible.

We'll assume that the files cannot be larger than 64KB

