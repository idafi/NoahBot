# NoahBot
A simple Discord bot, written in C#.

The NoahBot provides a few pretty-standard Discord bot features via text commands, in an effort to cull the growing proliferation of obnoxious utility bots on the server I frequent. Its name is sourced from a secondary goal to simulate much-loved but oft-absent friend and server mascot Noah.

Current features:
- Reddit link reader, reading, formatting, and parsing the top new/hot/etc. post of a given subreddit
- Activity selector, randomly selecting dynamically-weighted activities for bored and indecisive server members
- Greeting system, saying hi to a server when joined (or on command, for fun)

Planned features:
- Music player
- Automated RedditReader posting

---

# Building

You can build on Windows using either the provided Visual Studio 2017 solution, or the provided batch builders.

The project requires some external libraries:
- DSharpPlus, a .NET Discord API
- Json.NET, used by both DSharpPlus and the NoahBot.

A NuGet package configuration for these libraries is provided.

### Visual Studio
The C# layer uses .NET Framework 4.7, and several C# 7 features. If you're on older versions of Visual Studio, or a newer version that lacks the appropriate support, you'll need to set that up.

If that's in order, simply acquire the NuGet packages, and you should be good to go.

### Batch builders
The C# layer uses several C# 7 features, so you'll need a compiler that can handle those. No .NET Framework 4.7-exclusive features are currently used, but the VS build is configured as a 4.7 project; so, in case such features do find their way in, make sure you're appropriately updated.

You'll need to set up your local environment by altering the path variables in *util/setup_env.bat*. Library dependencies assume that libraries are acquired from NuGet and placed in a *lib/* directory at the project's base path. If you're getting them from somewhere else, you need to set up the appropriate paths in *util/setup.bat*.

Once your directories and libraries are appropriately configured, the *build.bat* script will set them up and start building the project.
