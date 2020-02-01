# ImageCaster [![matrix-members]][matrix] [![discord-members]][discord] [![gitlab-build]][gitlab] [![gitlab-coverage]][gitlab] [![donate-shield]][elypia-donate]
## About
ImageCaster is a small CLI application that can be used to manage
exporting images. It's primary use case is for CI/CD where a
repository that has a set of images as a source and programmatically
produces images as a result of a build.

An example and motivation for this is the [Elypia Emotes] repository
where we have many images, which we'd like exported in many
sizes and colors, as well as to dynamically generate montages to
display on the project README.

This project is not an alternative to [Magick.NET] or [ImageMagick], it
is a different way of interfacing with them. If you're looking for a
programming interface then you may prefer [Magick.NET], or if you're
looking for a tool executed purely from CLI, please visit [ImageMagick].
ImageCaster is an abstraction of the two to provide the features though
a declarative configuration file instead for simpler CI/CD usage as
well as some management for consecutive builds and quality assurance.

## Features
* Instead of scripting, define a declarative configuration that
describes the output you want.
* Quicker consecutive builds, ImageCaster will manage if changes
occurred and if re-exported again is required.
* Set [Exif] data for output, for example the `Copyright` or `Artist`
tag.
* Define patterns (directory/[glob]/[regex]) to match images and create
montages to display to users.
* Define patterns (directory/[glob]/[regex]) to match images and
archive images for download.
* Define checks to ensure your repository standards are maintained and
to avoid mistakes like mismatching names.

## Example
```yml
export:
  input: "src/static/panda(.+).png"
  metadata:
    exif:
      - tag: "Artist"
        value: "Elypia CIC and Contributors"
  sizes:
    filter: "Catrom"
    dimensions:
      - height: 512
      - height: 128
  colors:
    mask: "src/static/masks/panda$1.mask.png"
    modulate:
      - name: "blue"
        hue: 0
      - name: "violet"
        saturation: 70
        hue: 50
checks:
  file-exists:
    source: "src/static/panda(.+).png"
    target: "src/static/projects/panda$1.psd"
```
> First we define a pattern which matches all of our input images. For
> each image we add the Exif tag, `Artist`, and export 6 versions of
> each image to accommodate original, blue, and violet colors, in 512px,
> and 128px sizes. Doing `imagecaster build` will do all of this for
> you. You may wish to use `imagecaster check` first to perform all 
> checks, in this case only one check is defined which states if a mask
> exists, then an image must also exist with the respective name.

## Open-Source
This project is open-source under the [GNU General Public License]!  
While not legal advice, you can find a [TL;DR] that sums up what
you're allowed and not allowed to do along with any requirements if you
want to use or derive work from this source code!  

**All non-code files including videos, models, audio, bitmaps, vectors, and 
animations such as GIFs, are not under the aforementioned license; all rights
are reserved by Elypia CIC.** 

[matrix]: https://matrix.to/#/+elypia:matrix.org "Matrix Invite"
[discord]: https://discordapp.com/invite/hprGMaM "Discord Invite"
[gitlab]: https://gitlab.com/Elypia/imagecaster/commits/master "Repository on GitLab"
[elypia-donate]: https://elypia.org/donate "Donate to Elypia"
[Elypia Emotes]: https://gitlab.com/Elypia/elypia-emotes "Elypia Emotes"
[Magick.NET]: https://github.com/dlemstra/Magick.NET "Magick.NET on GitHub"
[ImageMagick]: https://github.com/ImageMagick "ImageMagick on GitHub"
[Exif]: https://en.wikipedia.org/wiki/Exif "Exif on Wikipedia"
[glob]: https://en.wikipedia.org/wiki/Glob_(programming) "Glob on Wikipedia"
[regex]: https://en.wikipedia.org/wiki/Regular_expression "Regular Expression on Wikipedia"
[GNU General Public License]: https://www.gnu.org/licenses/gpl-3.0.en.html "AGPL"
[TL;DR]: https://tldrlegal.com/license/gnu-general-public-license-v3-(gpl-3) "TL;DR of AGPL"

[matrix-members]: https://img.shields.io/matrix/elypia-general:matrix.org?logo=matrix "Matrix Shield"
[discord-members]: https://discordapp.com/api/guilds/184657525990359041/widget.png "Discord Shield"
[gitlab-build]: https://gitlab.com/Elypia/imagecaster/badges/master/pipeline.svg "GitLab Build Shield"
[gitlab-coverage]: https://gitlab.com/Elypia/imagecaster/badges/master/coverage.svg "GitLab Coverage Shield"
[donate-shield]: https://img.shields.io/badge/Elypia-Donate-blueviolet "Donate Shield"
