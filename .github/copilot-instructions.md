# GitHub Copilot Instructions for RimWorld Modding: Explosive Implant (Continued)

## Mod Overview and Purpose

The "Explosive Implant (Continued)" mod introduces a unique gameplay mechanic to RimWorld by adding explosive brain implants. These implants can be detonated at will, offering strategic control over pawns and their environment. This mod updates and extends the original exploitive implant functionality, maintaining compatibility with the current RimWorld version.

### Purpose
- To add a thrill of risk and reward to gameplay through specialized implants.
- To enhance strategic options during combat or emergency situations.

## Key Features and Systems

- **Explosive Implant:** A device implanted in a pawn’s brain that can be detonated on command.
  - Automatically results in the removal of the pawn's head upon explosion.
- **Advanced Implant:** Features twice the blast radius of the regular implant.
- **Knockout Implant:** Instantly sedates the pawn, offering a non-lethal alternative.
- **Corpse Explosion:** Implements the ability to detonate dead pawns.
- **Slave Terror Enhancement:** Slaves with explosive implants maintain 100% terror, adding a layer of psychological management.

## Coding Patterns and Conventions

- **C# Coding Standards:** Follow clean naming conventions and keep methods single-responsibility where possible.
  - Example: The `TerrorUtility_GetTerrorLevel` file uses postfix techniques to alter existing game functions.
- **XML Layout:** Consistently structured XML files for defining implants, recipes, and thoughts.
  - Maintain organized hierarchy and use descriptive naming for XML Defs for clarity.

## XML Integration

- **Defs:** Use XML files to define new recipes, hediffs, items, and thoughts associated with the implants.
- **Modular Approach:** Separate XML files for different types of definitions (HediffDefs, RecipeDefs, ThingDefs_Items).

## Harmony Patching

- **Harmony Dependency:** Utilize Harmony, a patching library for .NET languages, to apply runtime detours/changes to methods in the RimWorld assembly.
- **Postfix Patches:** Commonly used to extend existing methods with additional functionality without overriding the original behavior.
  - Example: The `TerrorUtility_GetTerrorLevel` file in the mod employs a postfix patch to modify terror levels for implanted slaves.

## Suggestions for Copilot

- **Variable Naming:** Provide meaningful context-based names to variables and methods to enhance readability.
- **Functionality Expansion:** Consider adding features such as remote-controlled detonation or tiered implant customization.
- **Error Handling:** Implement robust error and exception handling within C# patches to ensure mod stability.
- **Comments and Documentation:** Encourage the addition of comments and method summaries to improve maintainability and collaborative potential.

By integrating this mod, you can explore new tactical possibilities, engage players with unique challenges, and expand the narrative possibilities within the RimWorld universe.


This document serves as a guide for contributors and provides necessary insights into maintaining and expanding the mod’s functionality efficiently.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

