# FsReveal - Getting Started

Clone or [download](https://github.com/fsprojects/FsReveal/archive/master.zip) the [FsReveal repository](https://github.com/fsprojects/FsReveal).
The NuGet package alone is not enough.

## Prerequisites

If you are using Linux or OS X, [you need to install Mono](http://www.mono-project.com/download/ "Install Mono").

Windows users don't need to install anything.

## Edit your slides

Open `slides/input.md` in your favourite text editor.

This is the source for your entire presentation. Inside the file you will find an example presentation that demonstrates how to use FsReveal.
You can also read the <a href="formatting.html">formatting docs</a> to learn about FsReveal's Markdown format.

## Build

Open a console/terminal in the FsReveal folder.

If you're using Windows run 
    
    build.cmd
    
If you're using a Mac run

    ./build.sh
    
This will download all of the packages that FsReveal needs to create your slides.

Your slides are then generated and saved to the `output` folder.

A web server will start automatically and your presentation will be opened in your browser.

FsReveal will detect changes to your slides and generate them again for you. It even refreshes the browser.

## Use

- Use the arrow keys to navigate left, right, up and down
- Press `Esc` to see an overview
- Press `f` to view in fullscreen
- Press `s` to see speaker notes.

## Deployment

GitHub allows to host your slides. In order to deploy your slides make sure you have a `gh-pages` branch. 
If it doesn't exist, then just create and push it.
You'll need to edit build.fsx in order to modify gitOwner and gitProjectName appropriately.
After that you can release your software by simply running:
     
    $ build ReleaseSlides
