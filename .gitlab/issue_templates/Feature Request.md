# Feature Request
<!--
  This is for requesting new features, or improvements/additions
  to existing features.

  This is pre-filled with example values, feel free to
  remove them before populating the template.
  
  If you feel a heading is irrelevant, just remove it.
-->

## Description
<!-- 
  Explain what this is about, try to use full sentences, and make your point clear.
-->
I would like to be able to have a section in the configuration where I can store
and run CLI commands similar to the `package.json` with Node/NPM.

## Motivation
<!--
  Why is this a feature that should be implemented in ImageCaster?
-->
This leaves the full responsibility of running commands to the configuration
instead of having to divide up parts like commands between the configuration
and other `.sh` or a `.gitlab-ci.yml` file.

For example if a user wants to define a command to export a directory of images
into a GIF, however this is something ImageCaster doesn't support, we'd like to
write the command inside the `imagecaster.yml` file, and have the CI build do something
like `imagecaster run-script export-gif`.

This is also good because ImageCaster is usable between systems, while something like
`.gitlab-ci.yml` is only referenceable by the GitLab runner.

## Interface
<!--
  In some cases you may wish to propose an interface
  to help describe how you'd want to use this, or for others to discuss
  and improve ahead of time before final implementation.
-->
```yml
scripts:
  - name: "export-gif"
    command: "convert -delay 33 src/animated/pandaAww*.png pandaAww.gif"
export:
  input: "src/static/panda*.png"
  exif:
    - tag: "Artist"
      value: "Elypia CIC and Contributors"
  sizes:
    units: "px"
    dimensions:
      - height: 512
```

```
imagecaster run-script export-gif
```
