# PasswordManager

## Table of Contents

1. [PasswordManager](#passwordmanager)
2. [Starting the Program](#starting-the-program)
3. [The Password Page](#the-password-page)
4. [Discussion of the Product's Security](#discussion-of-the-products-security)
   - [Encryption Technologies Used](#encryption-technologies-used)
     - [Argon2id](#argon2id)
       - [Strengths](#strengths)
       - [Benefits](#benefits)
       - [Weakness](#weakness)
     - [AES](#aes)
       - [Strengths](#strengths-1)
       - [Benefits](#benefits-1)
       - [Weakness](#weakness-1)
   - [How It Works](#how-it-works)
     - [User Registration](#user-registration)
     - [Storing Normal Passwords](#storing-normal-passwords)
     - [Retrieving Normal Passwords](#retrieving-normal-passwords)
   - [Flaws and Potential Improvements](#flaws-and-potential-improvements)
5. [Conclusion](#conclusion)


Compulsory Project for Rasmus Thyregod Kristensen, PBSWe23: Second Semester Security Course.

You can start the program by clicking on the hollow green arrow: <img src="https://i.gyazo.com/993a800b3e576ad36234bcc81e94a516.png" alt="Alt Text">
You will be greeted by the login screen, right now only 1 master password can exist, so for this demo please use the demo password "1234"

<img src="https://i.gyazo.com/03b84f1cb147738fda571bddb29941b9.png" alt="Alt Text">

When logged in you will see the password page, this is where the passwords will be shown decrypted as they are stored encrypted in a MongoDB.

<img src="https://i.gyazo.com/d64085b6df575760e6c86cabe9f30576.png" alt="Alt Text">

From here you can add new passwords or click on the small arrow in the top left cornor to log out.

<img src="https://i.gyazo.com/4f5dd5c57f7c93203658804455ad83c6.png" alt="Alt Text">

You can type in the website, login info and password info into the text fields and hit the create button. The code behind the white & blue GUI encrypts both the login info and the password before storing them in the DB.

## Discussion of the products security.
Password managers play a crucial role in modern cybersecurity, providing a convenient and secure way for users to store and manage their passwords. 
This discussion focuses on the security mechanisms implemented in a custom password manager, analyzing their strengths, weaknesses, and potential areas for improvement.
### Encryption Technologies Used
#### Argon2id
Argon2id is a highly secure password hashing algorithm designed to protect sensitive data, 
particularly user passwords, from unauthorized access. 
It stands as the victor of the Password Hashing Competition (PHC) due to its superior security features. 
Combining elements from Argon2i and Argon2d, it offers robust resistance to various attack vectors, including GPU-based cracking, 
time-memory trade-off attacks, and side-channel attacks. Argon2id's core strength lies in its memory-intensive processing, 
adaptive parallelism, data-dependent passes, and constant-time implementations, 
making it a formidable choice for safeguarding passwords and sensitive information in a world where data breaches and cyber threats are ever-present concerns.

##### Strengths
Argon2id is a robust password hashing algorithm that is resistant to various attacks, including brute force, dictionary attacks, and rainbow table attacks. 
It uses a variable amount of memory and processing time, making it computationally expensive for attackers.
##### Benefits
Storing a hashed master password in MongoDB ensures that even if the database is compromised, the attacker cannot easily retrieve the original master password.
##### Weakness 
The strength of Argon2id depends on the chosen parameters (e.g., time cost, memory cost). Inadequate configuration of these parameters can weaken the protection.

#### AES
AES, or Advanced Encryption Standard, is a widely adopted and respected symmetric-key encryption algorithm. 
It's employed to secure sensitive data in various applications, from data storage to communication protocols. 
AES uses a block cipher with key lengths of 128, 192, or 256 bits, 
providing a high level of security. Its strength stems from its substitution-permutation network structure, 
which involves multiple rounds of complex operations, including substitution, permutation, and bitwise transformations. 
AES has been extensively analyzed and tested, 
proving its resistance to attacks like brute force and cryptanalysis. 
Its versatility and efficiency make it the de facto choice for data encryption across diverse domains.
##### Strengths
AES (Advanced Encryption Standard) is a widely recognized and trusted symmetric encryption algorithm. When configured and used correctly, it provides strong confidentiality for stored passwords.
##### Benefits
Encrypting normal passwords before storing them in MongoDB protects the user's sensitive data from unauthorized access, even if the database is compromised.
##### Weakness 
The security of AES depends on the strength of the encryption key. If the encryption key is weak or poorly managed, it can lead to vulnerabilities. 
and considering due to time restraint, the same key is used for all which is very bad security.

### How it works
#### User Registration
The user sets a master password, which is then securely hashed using Argon2id. The hashed master password is stored in MongoDB.
#### Storing Normal Passwords
When a user saves a password, it is encrypted using AES with a unique encryption key. The encrypted password is then stored in the database.
#### Retrieving Normal Passwords
To retrieve a password, the user provides the master password, which is hashed and compared to the stored hash in MongoDB. If they match, the master password is considered valid. 
The encryption key for the user's data is derived from the master password, which is then used to decrypt and display the stored passwords.

### Flaws and Potential Improvements
 - Given all passwords are encrypted used the same key and IV is a big security flaw and should be improved if the time allowed.
 - Master Password is hashed using Argon2id using itself as salt, which should be randomly generated instead and not stored in plaintext form.
 - The encryption key derived from the master password should be stored securely and protected against potential attacks. Additionally, implementing a mechanism for changing the master password and re-encrypting all stored passwords would enhance security.
 - Implementing monitoring and auditing mechanisms to detect and respond to unusual activities, such as multiple failed login attempts or suspicious database access, can improve overall security.

## Conclusion
The security mechanisms employed in this password manager provide a strong foundation for protecting user passwords. However, 
there are vulnerabilities related to user behavior and key management that need to be addressed for even greater security. 
Regular security assessments and updates are essential to maintaining the integrity of the system and ensuring that user data remains safe from potential threats.