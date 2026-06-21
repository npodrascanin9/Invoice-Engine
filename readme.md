# Mini API project
- MS SQL/.NET 8
- vertical-slice architecture, tests, design patterns

# Invoice Engine

## 1) What's the business problem?
This mini project belongs to the domain of transport and freight forwarding, specifically invoice management.  
When creating an invoice, the buyer and the seller as contractual parties agree on a **incoterm (obligation)** — who owes what percentage of the costs (invoice item types, such as: goods, transportation and insurance).

**Incoterm obligation rules table:**
<img width="1247" height="277" alt="image" src="https://github.com/user-attachments/assets/ba6051c8-e863-4a10-ac7e-96703562d4e1" />

**Example (case 1, CIF):**
- Buyer pays for goods
- Seller covers transport and insurance services.

**Example (case 2, EXW):**
- Buyers pays everything (goods, transport and insurance)
- Seller pays nothing

**Note:** In some cases, the buyer and seller can choose a **Custom** option, filling out an additional form to decide who owes what.

**Problem:** This business logic often leads to a large number of `if` conditions, which makes testing and maintenance harder. It is unpredictable what new requirements the client may introduce.

---

## 2) What's the solution to the problem?
The solution is to follow **SOLID principles** and apply the **Strategy pattern** instead of writing endless `if` statements.  
Each incoterm is implemented as a separate strategy, which allows:

- Easier unit testing for each strategy.
- Simple extension when new incoterms are introduced.
- Clean and maintainable code.

**Database model:**
<img width="1140" height="758" alt="image" src="https://github.com/user-attachments/assets/e94a73a1-59ec-4895-8341-f8bdff09fdec" />

**Tables:**
****1) Clients**** - Stores company details (name, identification number, email, isActive)
****2) InvoiceClients**** - Composite key table linking invoices to clients. Each client has a single role (Buyer/Seller) per invoice
****3) Invoices**** - Contains invoice metadata, including IncotermCode column which is the main focus in this mini project
****4) InvoiceItems**** - Holds items with type (Transportation, Goods, Insurance), amount, and description 
****5) InvoiceItemObligations**** -  Composite key table defining obligations (who owes whom and how much)
****6) CustomIncotermObligations**** - Composite key table for custom incoterm agreements, ensuring obligations are recorded for future dispute resolution

---

## 3) What if the client has new requirements?
Example: the client requests a new incoterm rule **CFR - Cost and Freight**.  
- Buyer pays 100% for goods and insurance.  
- Seller pays 100% for transporation.

**Solution:** Add a new strategy to the Strategy pattern.  
**Advantage:** No need to modify existing code or add new `if` conditions. The system remains extensible and testable.

---

## 4) Conclusion
- There are multiple ways to solve this problem:
  - **Database tables + user function** to calculate obligations.  
    - Pros: centralized logic.  
    - Cons: additional queries, harder to test.
  - **Strategy pattern in code**.  
    - Pros: easier testing, clean code, extensible.  
    - Cons: requires more classes, but ensures maintainability.

- The code is **easily extensible** and follows **SOLID principles**.  
- When the client introduces new requirements, the problem is solved by adding a new strategy without breaking the existing system.  
- This project demonstrates understanding of the **financial domain (obligations)** and the application of **enterprise practices** (CQRS, Strategy, integration testing with Testcontainers).

