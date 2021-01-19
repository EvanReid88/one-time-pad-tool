# One Time Pad Encryption Tool

This is a console tool created with .NET Core 3.0 for encrypting/decrypting files using a one-time-pad. 
The program allows you to the one-time-pad as a base64 string txt file and securely deletes original files.

## Features
- Encrypt files with a one time pad. 
- Securely delete original file after encryption
- Save pad as raw file (with same extension as original file)
- Save pad as base64 encoded string in a .txt file
- Decrypt files with a raw pad or base64 encoded .txt file pad
- Delete pad and decrypted file after decryption

## NuGet Packages
- BouncyCastle.NetCore
- EasyConsoleCore