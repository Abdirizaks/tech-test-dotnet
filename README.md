# Zak Ismail's Refactor

## Confirmed Functionalities
 - [x] Lookup the account the payment is being made from
 - [x] Check the account is in a valid state to make the payment
 - [x] Deduct the payment amount from the account's balance and update the account in the database
 
## Refactored With The Following In Mind
 - [x] Adherence to SOLID principals
 - [x] Testability  
 - [x] Readability 

## Verified Outcomes
 - [x] The solution builds.
 - [x] Tests alinged with business logic all pass.
 - [x] Unchanged method signature of the MakePayment method.

## Changes
`AccountDataStore` and `BackupDataStore` were not test friendly. 
- [x] Added `IDataStore` which helped with returning mocks in unit tests.

Made use of Factory Pattern so the right data store is selected.
- [x] Added `IDataStoreFactory`.

Introduced a validator as the payment service did not need to include validation logic (*S*OLID).
- [x] Added `IPaymentValidator`.

Separated the data store from the payment service (*S*OLID).
- [x] Added `IAccountDataStoreService`.

- Added unit tests to validate business logic.
- [x] Added `AccountDataStoreServiceTests`, `PaymentServiceTests` and `PaymentValidatorTests`.

## If I Had More Time
1. I would have separated the validation logics into what pertains to each PaymentScheme. Why ? because this would more readable and result in a cleaner test class.

2. I would written tests for `DataStoreFactory` to ensure the right datastore is being picked based on config value.

Yours Truly,
~~Ariana Grande~~ Zak Ismail

