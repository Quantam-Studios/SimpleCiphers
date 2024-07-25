# Simple Ciphers
A C# package for plain text encryption and decryption for simple, but fun ciphers.
# Add to Project
Via `dotnet`:
```cs
dotnet add package simple-ciphers --version 2.3.0
```
Via `PackageReference` in your `.csproj` file:
```cs
<PackageReference Include="simple-ciphers" Version="2.3.0" />
```
# `Encryption`
Use `A1Z26()` to substitute letters for their corresponding positions.
```cs
Encryption.A1Z26("hello world!");
// returns: 8-5-12-12-15 23-15-18-12-4!
```
Use `Atbash()` to swap letters to their opposite positions.
```cs
Encryption.Atbash("hello world!");
// returns: svool dliow!
```
Use `Caesar()` for shift ciphers.
```cs
Encryption.Caesar("hello world!", 3);
// returns: khoor zruog!
```
Use `Morse()` to substitute letters for their morse code equivalent.
```cs
Encryption.Morse("hello world!");
// returns: .... . .-.. .-.. --- / .-- --- .-. .-.. -.. -.-.--
```
Use `Vigenere()` to cipher with a key.
```cs
Encryption.Vigenere("hello world!", "key");
// returns: rijvs uyvjn!
```
Use `Binary()` to cipher with Binary.
```cs
Encryption.Binary("hello world!");
// returns: 01101000 01100101 01101100 01101100 01101111 00100000 01110111 01101111 01110010 01101100 01100100 00100001
```
# `Decryption`
Use `A1Z26()` to substitute numbers for their corresponding letters.
```cs
Decryption.A1Z26("8-5-12-12-15 23-15-18-12-4!");
// returns: hello world!
```
Use `Atbash()` to swap letters to their opposite positions.
```cs
Decryption.Atbash("svool dliow!");
// returns: hello world!
```
Use `Caesar()` for shift ciphers.
```cs
Decryption.Caesar("khoor zruog!", 3);
// returns: hello world!
```
Use `Morse()` to substitute morse code for their letter and/or symbol equivalent.
```cs
Deryption.Morse(".... . .-.. .-.. --- / .-- --- .-. .-.. -.. -.-.--");
// returns: hello world!
```
Use `Vigenere()` to decipher with a key.
```cs
Deryption.Vigenere("rijvs uyvjn!", "key");
// returns: hello world!
```
Use `Binary()` to decipher with Binary.
```cs
Deryption.Binary("01101000 01100101 01101100 01101100 01101111 00100000 01110111 01101111 01110010 01101100 01100100 00100001");
// returns: hello world!
```
