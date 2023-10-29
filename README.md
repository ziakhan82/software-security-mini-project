# PassManagerMiniPorject
## Technology Stack

**Programming Language:** C#
**Application Type:** Console Application

 ## Getting Started
- downlad or clone the repository
- run the application

   3.**Log In:**
- The application will prompt you to enter your master password.   the master password is : Ass12345#$

   4. **Use the Password Manager:**
- Once logged in, you can use the following commands:
  - `g`:Press 'g' to generate a password for a service
  - `q`: Quit the application.
    
![image](https://github.com/ziakhan82/software-security-mini-project/assets/65169811/b82c9432-90bf-4420-b1ab-3d0b4c63566f)

![image](https://github.com/ziakhan82/software-security-mini-project/assets/65169811/cde397e5-d39d-44d1-ae7a-cf0376d6dc11)

when you are done simply press the q button it will close the application


 # Security:
**BCrypt for Password Encryption**:

BCrypt is used to securely store the passwords. It provides a strong one-way hashing function that makes it computationally intensive for attackers to perform a brute-force or dictionary attack.
When a user sets their master password, the application hashes it using BCrypt and stores the resulting hash in a file. This ensures that the raw master password is never stored directly.
During login, the entered master password is hashed using the same salt and compared to the stored hash. This process prevents attackers from gaining access to the master password, even if they gain access to the stored hashes.
Key Derivation for Encryption (Possibly PBKDF2): The security of the app depends on the complexity of the master password and the length of the derived encryption key. I Encourage users to choose strong, unique master passwords.

 **PBKDF2 (Password-Based Key Derivation Function 2).**
PBKDF2 is used to derive encryption key from the master password. It introduces computational complexity by performing multiple iterations of a hash function, making it resilient to brute-force attacks.
In the application, the hashed master password and salt are used as inputs to PBKDF2 to derive an encryption key. This key is used to encrypt and decrypt stored passwords.The number of iterations should be set high enough to slow down any brute-force or dictionary attacks.

**Using Encryption Key for Password Storage**

When creating a password for a service, the application encrypts it using the encryption key derived from the master password.
By using a derived encryption key, the application ensures that the stored passwords are protected. Even if an attacker gains access to the stored passwords, they cannot decrypt them without the correct encryption key, which depends on the user's master password.

**Key Matching for Decryption**

During the decryption process (e.g., when retrieving stored passwords), the same encryption key derived from the master password is used for decryption. This ensures that only users with the correct master password can access their stored passwords.

**future feature**
potential enhancement for a future update could involve implementing features that allow users to modify and remove previously stored credentials for increased flexibility and control.The security aspects of the application could be significantly enhanced by migrating to cloud-based solutions with robust security measures. Currently, the sensitive information is stored in a JSON file, which may not provide the highest level of security. Additionally, improving user experience and security could involve hiding passwords during both the password creation and login processes.

**Secure Key Storage:**  Protect the encryption key derived from the master password. in the future Use secure key storage solutions like hardware security modules (HSMs) to protect the key.

**Multi-Factor Authentication (MFA):** Implement MFA for added protection. Even if an attacker gains access to the hash and salt file, MFA can prevent unauthorized access to user accounts.

This feature was unintentionally overlooked due to time constraints during the project's development.


In summary, BCrypt provides strong password hashing, making it challenging for attackers to guess the master password. PBKDF2 enhances security by deriving the encryption key from the master password, making it difficult for attackers to decrypt stored passwords. These techniques collectively protect user data and add layers of security to the application.
