# Contributing to Peyghom

Thank you for your interest in contributing to Peyghom! This document provides guidelines and instructions for contributing to this project. By participating in this project, you agree to abide by these guidelines.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Workflow](#development-workflow)
- [Pull Request Process](#pull-request-process)
- [Coding Standards](#coding-standards)
- [Commit Message Guidelines](#commit-message-guidelines)
- [Testing](#testing)
- [Documentation](#documentation)
- [Issue Reporting](#issue-reporting)
- [Feature Requests](#feature-requests)
- [Communication](#communication)

## Code of Conduct

Our project is committed to providing a welcoming and inspiring community for all. Please read and follow our [Code of Conduct](CODE_OF_CONDUCT.md) to ensure a positive experience for everyone involved.

## Getting Started

1. **Fork the repository** on GitHub
2. **Clone your fork** to your local machine:
   ```bash
   git clone git@github.com:Hadi-bakhshi/Peyghom.git
   cd Peyghom
   ```
3. **Add the original repository as an upstream remote**:
   ```bash
   git remote add upstream git@github.com:Hadi-bakhshi/Peyghom.git
   ```
4. **Set up the development environment**:
   - **With Docker** (recommended):
     ```bash
     docker-compose up -d
     ```
   - **Without Docker**:
     ```bash
     dotnet restore
     ```
5. **Create a new branch** for your changes:
   ```bash
   git checkout -b feature/your-feature-name
   ```

## Development Workflow

### Modular Monolith Architecture

Peyghom follows a modular monolith architecture. Each module should:

- Be isolated with clear boundaries
- Have its own domain models, services, and repositories
- Communicate with other modules through well-defined interfaces

When contributing, respect the existing module boundaries and avoid creating unnecessary dependencies between modules.

### Module Structure

Each module should follow this structure:
```
```

### Local Development

#### Using Docker (Recommended)

1. Make your changes in your feature branch
2. Build and run the application using Docker:
   ```bash
   docker-compose up --build
   ```
3. Run tests inside the Docker container:
   ```bash
   docker-compose exec api dotnet test
   ```
4. Access the application at `http://localhost:PORT`

#### Without Docker

1. Make your changes in your feature branch
2. Build the project to ensure it compiles:
   ```bash
   dotnet build
   ```
3. Run tests to ensure they pass:
   ```bash
   dotnet test
   ```
4. Run the application locally:
   ```bash
   dotnet run --project src/API/Peyghom.Api
   ```

## Pull Request Process

1. **Update your fork** with the latest changes from the upstream repository:
   ```bash
   git fetch upstream
   git merge upstream/main
   ```
2. **Commit your changes** with clear commit messages (see [Commit Message Guidelines](#commit-message-guidelines))
3. **Push your changes** to your fork:
   ```bash
   git push origin feature/your-feature-name
   ```
4. **Submit a pull request** to the main repository
5. **Update the PR description** with details of your changes, the motivation, and any additional context
6. **Address review comments** if requested by maintainers
7. Once approved, your PR will be merged by a maintainer

### PR Requirements

For a PR to be accepted, it must:

- Pass all automated tests
- Follow the project's coding standards
- Include appropriate tests for new functionality
- Update documentation if necessary
- Be focused on a single concern/feature

## Coding Standards

Peyghom follows the standard .NET coding conventions and best practices.

### C# Coding Style

- Use 4 spaces for indentation
- Follow [Microsoft's C# coding conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use [StyleCop](https://github.com/DotNetAnalyzers/StyleCopAnalyzers) rules

### Naming Conventions

- **PascalCase** for class names, method names, public properties
- **camelCase** for local variables, private fields
- Prefix private fields with underscore (`_privateField`)
- Use descriptive names that convey intent

### Design Principles

- Follow SOLID principles
- Favor composition over inheritance
- Use dependency injection for loose coupling
- Keep methods small and focused on a single responsibility
- Avoid static classes and methods when possible
- Design services to be container-friendly and environment-agnostic
- Follow the twelve-factor app methodology for containerized applications

## Commit Message Guidelines

We follow the [Conventional Commits](https://www.conventionalcommits.org/) specification:

```
<type>(<scope>): <subject>

<body>

<footer>
```

### Type

- **feat**: A new feature
- **fix**: A bug fix
- **docs**: Documentation only changes
- **style**: Changes that do not affect the meaning of the code (formatting)
- **refactor**: A code change that neither fixes a bug nor adds a feature
- **perf**: A code change that improves performance
- **test**: Adding missing tests or correcting existing tests
- **chore**: Changes to the build process or auxiliary tools

### Scope

The scope should be the name of the module affected.

### Examples

```
feat(messaging): add support for message reactions
```

```
fix(video-call): resolve issue with camera initialization
```

## Testing

Every contribution should include appropriate tests:

- **Unit tests** for individual components
- **Integration tests** for component interactions
- **End-to-end tests** for critical user flows

### Testing Guidelines

- Aim for high test coverage on new code
- Use xUnit for testing
- Follow the Arrange-Act-Assert pattern
- Use meaningful test names that describe the behavior being tested
- Mock external dependencies using a mocking framework like Moq

## Documentation

Documentation is crucial for the project:

- Update the README.md if necessary
- Include XML documentation comments for public APIs
- Update or create module-specific documentation
- For significant changes, update architecture documentation
- Document Docker setup and configuration changes
- Keep the Docker-related documentation up-to-date (Dockerfile, docker-compose.yml)

## Issue Reporting

When reporting issues, please:

1. Check if the issue already exists in the project's issue tracker
2. Use the provided issue template
3. Include clear reproduction steps
4. Specify your environment (OS, .NET version, etc.)
5. Include relevant logs or screenshots

## Feature Requests

For feature requests:

1. Check if the feature has already been requested
2. Use the feature request template
3. Provide a clear description of the proposed feature
4. Explain the benefit of the feature to the project
5. Consider submitting a PR to implement the feature yourself

## Communication

- **GitHub Issues**: Primary channel for bug reports and feature requests
- **Pull Requests**: For submitting contributions
- **Discussions**: For general questions and discussions about the project

---

Thank you for contributing to Peyghom! We appreciate your time and effort to help make this project better.
