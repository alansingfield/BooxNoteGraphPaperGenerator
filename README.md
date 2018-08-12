# BooxNoteGraphPaperGenerator
Generates graph paper in specific format for Boox Note eReader

I thought I would share some of my experiences with creating custom noteTemplate .png files - it's tricker than I first thought.

I'm after two main styles:

    Graph paper with 2cm outer grid, 1cm inner grid, 2mm tiny grid.
    Lined writing paper with margin (like you had in school).


First attempt was to find an online PNG graph generator, generate a graph and copy it to noteTemplates. I set the output size to exactly 1404px wide by 1872px high.

The result was awful! About every 5th or 6th line was missing or narrower than it should be. However, pressing the full-screen button made it look perfect.

So - I can conclude from this that it does a destructive scaling of the image in the view with the toolbar / menubar, then shows at native resolution when you do full screen.

Next challenge was to find out WHICH pixel rows and columns get missed out. Time to get out the software development tools

I created a PNG a little bit like those ones that inkjet printers do to find a blocked nozzle, and loaded that into noteTemplates.

After a lot of counting and fiddling around with Excel, I found that about every 16th/17th pixel gets missed out.

So then I could create a graph drawing program that will look at each line it is going to draw, compare to the list of "dead" pixels - if it is a dead one then shift back or forwards by 1px.

Loaded up into noteTemplate and we have a nice set of clean backgrounds that look good in normal edit or full screen! 


https://www.mobileread.com/forums/showthread.php?p=3736086#post3736086

