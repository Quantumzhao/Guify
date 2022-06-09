# Guify

This is a tool to generate a simple GUI for a CLI command. The user would only need to supply a simple XML file defining the expected representation of various flags and verbs in such a CLI command. 

In such way, users who have a hard time memorizing things (just like me), can always rely on the GUI until they get familiarized. 

## XML file

The extension for such files are `.gui`. They can be placed anywhere, as long as it is configured. 

This tool also comes with a set of XSD schemas to help users create valid XML documents. They are located in `./UI/Schemas`

### Supported features

The following table is the recommended layout. 

| *nix style CLI Grammar                                       | XML Node                                            |
| ------------------------------------------------------------ | --------------------------------------------------- |
| Verb                                                         | `Page`                                              |
| Flags (with short/long name) with **no** argument            | `Checkbox`                                          |
| Flags (with short/long name) with `string`/`int`/`float` argument | `TextBox`/`NumberBox`                              |
| Flags (with short/long name) with path-to-file/directory argument | `OpenFolderBox`/`OpenFileBox`/`SaveFileBox`         |
| Arguments with no flags                                      | `TextBox`, although users have to supply a comment |
| Mutually exclusive flags                                     | `RadioButton` wrapped in `Group`                    |
| Flags with constraints (e.g. some other flags must be `false` if this flag is `true`) | `CheckBox` but with extra constraints               |
| Flags with choices                                           | `ListBox`                                           |