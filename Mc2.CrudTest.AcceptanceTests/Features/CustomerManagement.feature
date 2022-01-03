Feature: CustomerManagement

@mytag
Scenario: Create valid customer successfully
When I send following customer details 
		| FirstName | LastName  | DateOfBirth  | PhoneNumber | Email                       | BankAccountNumber      |
		| Shohreh   | Mortazavi | "1981-06-22" | 09394638843 | shohreh_mortazavi@gmail.com | NL91 ABNA 0417 1643 00 |
		| Sara      | Ahmadi    | "1991-01-03" | 09123232323 | Sara.Ahmadi@yahoo.com       | NL91 ABNA 0317 1443 40 |
		| Reza      | Sadeghi   | "1967-11-15" | 09021112212 | rezasadeghi@hotmail.com     | NL91 ABNA 2417 1643 01 |
Then Customers are created successfully