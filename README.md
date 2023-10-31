# Simple Ciphers
A C# package for plain text encryption and decryption for simple, but fun ciphers.
# Encryption
Use `Caesar()` for shift ciphers.
```cs
Encryption.Caesar("hello world!", 3);
// returns: khoor zruog!
```
Use `Atbash()` to swap letters to their opposite positions.
```cs
Encryption.Atbash("hello world!");
// returns: svool dliow!
```
Use `A1Z26()` to substitute letters for their corresponding positions.
```cs
Encryption.A1Z26("hello world!");
// returns: 8-5-12-12-15 23-15-18-12-4!
```
Use `Morse()` to substitute letters for their morse code equivalent.
```cs
Encryption.Morse("hello world!");
// returns: .... . .-.. .-.. --- /  .-- --- .-. .-.. -.. -.-.--
```
# Decryption
Not yet implemented.
