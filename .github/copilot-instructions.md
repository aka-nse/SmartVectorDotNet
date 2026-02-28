# Copilot Instructions for SmartVectorDotNet

## What's this file for?

- This file provides instructions for generating source or operating as an agent
  for this repository to GitHub Copilot or other AI code assistants.
- When implementing new features, please use the technology selection,
  design policy, and module configuration shown here as a premise.
- If in doubt, explore the files in the repository and ask the user "Is this
  what you meant?"

## Preconditions

- The comments or documentations in the source codes must be in English unless
  there is specific instructions.
- The answer in the shell writes in user local language as possible as.
- When making code changes, if the number of lines modified is likely to exceed
  200, please prompt the user beforehand with the message: "This instruction may
  result in over 200 lines of code being modified. Do you wish to proceed?"
- When making significant changes, first create a plan outlining what you intend
  to do, then propose it to the user by saying, "We plan to proceed with this
  approach." If the user requests modifications to the plan at this stage,
  revise the plan accordingly and resubmit it.

## Project overview

SmartVectorDotNet is a .NET library designed to provide efficient and flexible
vector operations for various applications, including machine learning,
scientific computing, and data analysis. The library offers a range of vector
types and operations, optimized for performance and ease of use.

Please refer to the [README.md](../README.md) for more details about the
project, including its features, installation instructions, and usage examples.

## Directories and files

This project has the following directory structure:

```
./
+-- .editorconfig                     # Editor configuration file
+-- .gitignore                        # Git ignore file
+-- LICENSE.txt                       # License information for the project
+-- Measure-Coverage.ps1              # PowerShell script for measuring code coverage
+-- SmartVectorDotNet.slnx            # Visual Studio solution file
+-- README.md                         # Project overview and documentation
+-- global.json                       # .NET SDK version configuration
+-- .github/
|   +-- workflows/                    # GitHub Actions workflow files
|   |   +-- dotnet-CI.yml             # CI workflow for .NET builds and tests
|   +-- copilot-instructions.md       # This file: instructions for GitHub Copilot
+-- resources/                        # Misc. resource files for the project
+-- src/                              # Source code for the library
|   +-- SmartVectorDotNet/            # Main library code|
+-- tests/                            # Unit tests for the library
|   +-- SmartVectorDotNet.Tests/      # Unit tests for the library
+-- sample/                           # Sample applications demonstrating library usage
|   +-- SmartVectorDotNet.Example/    # Sample console application
+-- experiments/                      # Experimental code and prototypes
```

## Coding style

- Unless specific commented, the coding style should follow .editorconfig file.
- Unless specific commented, the coding style should follow the [Microsoft C# coding conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).
- All of public members must have XML documentation comments.
