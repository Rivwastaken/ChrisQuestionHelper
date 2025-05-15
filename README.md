# ChrisQuestionHelper

A simple C# application created for a teacher to generate exam questions randomly. :contentReference[oaicite:0]{index=0}

## Features

- Reads questions from a CSV file (`questions.txt`).
- Randomly selects a specified number of questions.
- Supports filtering by subject and/or year level (edit the CSV or add CLI options).
- Outputs the selection to the console and (optionally) to a file.

## Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download) or later  
- Visual Studio 2022 (or any IDE with C# support) **or** the `dotnet` CLI

## Installation

1. **Clone** this repository:
   ```bash
   git clone https://github.com/Rivwastaken/ChrisQuestionHelper.git
   cd ChrisQuestionHelper

## Questions.txt Format

```Subject,YearLevel,Question
Geometry,8,What is the area of a triangle?
Geometry,9,Prove that the sum of angles in a triangle is 180 degrees.
Trigonometry,10,What is the sine of 30 degrees?
