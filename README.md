# OpenXMLExample.ReplaceCustomXML #

Example project which takes a .docx file, removes the CustomXMLParts and adds a new one.

## Usage ##

Open in Visual Studio and hit F5. Now there's a file called `Out.docx` inside the project folder with a new name inside that differs from
the template `Assets/Template.docx`.

It's a CLI program that takes two arguments (template path and output path), and the launch profile in Visual Studio is already
configured to use `Assets/Template.docx Out.docx` as arguments.

## License ##

MIT
