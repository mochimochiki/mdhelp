---
title: Generate chm help from Markdown (Google Translate)
weight: 10
---

### Files needed to compile help

* project.hhp: Project file
* toc.hhc: Table of contents generation file
* alias.h: Alias ​​definition file

### Restrictions

* File name should be ASCII characters
  * When the URL is generated, it will be encoded as Japanese and the table of contents generation will not work.
  * Because the management cost will increase if the file name is different for each language

### baseof.html

[Create a base template to unify the basic configuration of various templates | Makumaku Hugo/Go Note](https://maku77.github.io/hugo/template/base-template.html)


### toc.hhc

toc.hhc is needed to generate the table of contents.

```
[mediaTypes]
  [mediaTypes."text/hhc"]
    suffixes = ["hhc"]
[outputFormats]
  [outputFormats.HHC]
    baseName = "toc"
    isPlainText = true
    mediaType = "text/hhc"
```

If you do as above and put index.hhc in layouts, you can generate a file of any format like toc.hhc.
"Reference": https://qiita.com/httpd443/items/1bd19ad4f7b96876b27f

* index.hhc is implemented based on "here": https://maku77.github.io/hugo/list/page-hierarchy.html

* By creating a site for each /jp,/en and setting the baseURL to ".", the link of toc.hhc could be generated automatically.
* If I didn't enter the baseURL, the "/" would go in first and I had to figure out a way to remove it.
* As another means, you can think of deleting "/" separately after generating toc.hhc, but it's good because it was done with HUGO.

### project.hhp

project.hhp is the project file. Compile for this.


## I want to change the contents subtly by edition

Almost the same, but I want to change the text slightly depending on the edition. I would like to realize by switching some config.toml by putting some flags in the front matter of the manuscript and corresponding parts.

### Section level unnecessary

If you set `ignoreFiles = ["ignore"]`, /content/jp/ignore/ and the following will be ignored by HUGO. OK if you write config.toml for each edition.

### Show/hide at article level

### Display/hide at the sentence level

### Reference

* [Ignore Content Files When Rendering](https://gohugo.io/getting-started/configuration/#ignore-content-files-when-rendering)
* [Ignore a directory](https://discourse.gohugo.io/t/ignore-a-directory/8880)
* [front-matter/#user-defined](https://gohugo.io/content-management/front-matter/#user-defined)
* [Shortcode output only when the conditions are met] (https://maku77.github.io/hugo/shortcode/private.html)