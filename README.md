# mdhelp

mdhelpはHTML Help(.chm)のヘルプを生成することを目的とした[HUGO](https://gohugo.io/)のテーマです。テーマ本体に加え、HTML Help生成に利用するツールなどを含んでいます。HTML Helpの生成には別途[HTML Help Workshop](https://docs.microsoft.com/en-us/previous-versions/windows/desktop/htmlhelp/microsoft-html-help-downloads)が必要です。

詳しい使い方は[mdhelp Docs](https://mochimochiki.github.io/mdhelp/jp/)を参照してください。

## 使い方

### 前提条件

1. [HUGO](https://gohugo.io/)をダウンロード・インストールし、`hugo.exe`にPATHを通しておきます。
1. [Microsoft HTML Help Downloads](https://docs.microsoft.com/en-us/previous-versions/windows/desktop/htmlhelp/microsoft-html-help-downloads)から`Htmlhelp.exe`をダウンロード・インストールし、`hhc.exe`にPATHを通しておきます。

### Clone

```
git clone https://github.com/mochimochiki/mdhelp.git
cd mdhelp
```

### ExampleSiteをコンパイル

以下のバッチを実行すると2種類のconfigでHUGOのビルド及びヘルプの生成が行われます。

```
.\exampleSite\CI\build.bat
```

以下が成果物です。

* `.\exampleSite\public_chm\jp\mdhelp.chm`
* `.\exampleSite\public_chm\en\mdhelp.chm`

## 制約

### 原稿のファイル名

原稿のmdファイル名は英数字とパーセントエンコーディングされない記号のみ対応しています。日本語など、URLが[パーセントエンコーディング](https://ja.wikipedia.org/wiki/%E3%83%91%E3%83%BC%E3%82%BB%E3%83%B3%E3%83%88%E3%82%A8%E3%83%B3%E3%82%B3%E3%83%BC%E3%83%87%E3%82%A3%E3%83%B3%E3%82%B0)されるファイル名の場合、chmコンパイル時にエラーが発生します。
