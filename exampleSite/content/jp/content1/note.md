---
title: Markdownからchmヘルプを生成する
weight: 1
---

### ヘルプをコンパイルするのに必要なファイル

* project.hhp : プロジェクトファイル
* toc.hhc : 目次生成用ファイル
* alias.h : エイリアス定義ファイル

### 制約事項

* ファイル名はASCII文字とする
  * URL生成時に日本語だとエンコードされてしまい、目次生成がうまくいかないため
  * 言語ごとにファイル名が違うと管理コストが上がるため

### baseof.html

[ベーステンプレートを作成して、各種テンプレートの基本構成を統一する | まくまくHugo/Goノート](https://maku77.github.io/hugo/template/base-template.html)


### toc.hhc

toc.hhcは目次を生成するために必要。

<pre>
[mediaTypes]
  [mediaTypes."text/hhc"]
    suffixes = ["hhc"]
[outputFormats]
  [outputFormats.HHC]
    baseName = "toc"
    isPlainText = true
    mediaType = "text/hhc"
</pre>

のようにしておき、layoutsにindex.hhcを入れておくことでtoc.hhcのように任意の形式のファイルが生成可能。
"参考":https://qiita.com/httpd443/items/1bd19ad4f7b96876b27f

* index.hhcは"ここ":https://maku77.github.io/hugo/list/page-hierarchy.html を参考に実装

* /jp,/en それぞれでサイトを生成し、baseURLを"."にしておくことでtoc.hhcのリンクを自動生成できた。
* baseURLを入れないと"/"が最初に入ってしまい、これを消す手段を考えなくてはならなかった。
* 別の手段としてはtoc.hhc生成後に"/"を別途消す方法も考えられるが、HUGOでできたので良しとする。

### project.hhp

project.hhpはプロジェクトファイル。これを対象にコンパイルを実施する。


## エディションで内容を微妙に変えたい

ほぼ同じだが、エディションによって微妙に文章を変更したい。原稿のフロントマターや該当箇所に何らかのフラグを入れておき、config.tomlを切り替えることで実現したい。

### セクションレベルで不要なもの

`ignoreFiles = ["ignore"]` とすれば/content/jp/ignore/ 以下がHUGOに無視される。エディションごとにconfig.tomlを書き分ければOK。

### 記事レベルでの表示/非表示

### 文章単位レベルでの表示/非表示

### 参考

* [Ignore Content Files When Rendering](https://gohugo.io/getting-started/configuration/#ignore-content-files-when-rendering)
* [Ignore a directory](https://discourse.gohugo.io/t/ignore-a-directory/8880)
* [front-matter/#user-defined](https://gohugo.io/content-management/front-matter/#user-defined)
* [条件を満たすときだけ出力するショートコード](https://maku77.github.io/hugo/shortcode/private.html)


