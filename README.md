# Simple Ciphers
A C# package for plain text encryption and decryption for simple, but fun ciphers.
# Add to Project
Via `dotnet`:
```cs
dotnet add package simple-ciphers --version 2.0.5
```
Via `PackageReference` in your `.csproj` file:
```cs
<PackageReference Include="simple-ciphers" Version="2.0.5" />
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
Encryption.Vigenere("rijvs uyvjn!", "key");
// returns: hello world!
```