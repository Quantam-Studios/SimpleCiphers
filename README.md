# Simple Ciphers
A C# package for plain text encryption and decryption for simple, but fun ciphers.
# Add to Project
Via `dotnet`:
```cs
dotnet add package simple-ciphers --version 1.0.0
```
Via `PackageReference` in your `.csproj` file:
```cs
<PackageReference Include="simple-ciphers" Version="1.0.0" />
```
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
