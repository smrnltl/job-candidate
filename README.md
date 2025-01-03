# job-candidate

Assumptions:
1. Email is the unique identifier for candidates.
2. Candidate information includes fields: FirstName, LastName, Email, PhoneNumber, CallTimeInterval, LinkedIn, GitHub, and Comment.
3. The application will use InMemory database for simplicity but can be scaled to other databases.
4. Caching will reduce database lookups for existing candidates.
5. The repository pattern is introduced to enable future extension or migration of storage backend.

Improvements:
1. Fluent Validation
2. Error handling for database save failures.
