---
title: "Getting Started"
weight: 10
---

## 準備

[HUGO](https://gohugo.io/)と[HTML Help Workshop](https://www.microsoft.com/en-us/download/details.aspx?id=21138)のインストールがまだであれば、まずはインストールを行います。その後、`hugo.exe`および`hhc.exe`に`PATH`を通しておきます。

## インストール

新しいプロジェクトを作成します。ここでは`myhelp`という名称としてみましょう。

```
hugo new site myhelp
cd myhelp
```

### MDHELPテーマをインストールする

ここでは[HUGO公式ドキュメント](https://gohugo.io/getting-started/quick-start/#step-3-add-a-theme)に沿ってgitでテーマを取得します。

```
git init
git submodule add https://github.com/mochimochiki/mdhelp themes/mdhelp
```

gitを利用しない場合、[最新のzip](https://github.com/mochimochiki/mdhelp/archive/master.zip)もしくは[任意のRelease](https://github.com/mochimochiki/mdhelp/releases)をダウンロード・解凍し、`themes\mdhelp`に配置してください。

### 必要なファイルをコピーする

以下のコマンドでヘルプのビルドに必要なツール類とconfigをコピーして`myhelp`プロジェクトに配置します。また、デフォルトのconfig.tomlは削除しておきます。

```
xcopy /s themes\mdhelp\exampleSite\config config\
xcopy /s themes\mdhelp\exampleSite\CI CI\
rm config.toml
```

## 最初のサイトの作成

### config.tomlの編集

`config\_default\config.toml`を開き、以下の行をコメントアウトもしくは削除します。

```toml
themesdir = "../.."
```

また、日英ともにサイト・ヘルプのタイトルを`MyHelp`に変えておきます。各言語の`title`の設定を以下のように編集します。

```toml
[languages.jp]
  ...
  title = "MyHelp"
  ...
[languages.en]
  ...
  title = "MyHelp"
  ...
```

その他のconfigパラメータについては[HUGO公式ドキュメント](https://gohugo.io/getting-started/configuration/)などを参照してください。

### トップページの作成

`content`ディレクトリ下に`jp`,`en`ディレクトリを作成します。

```
mkdir content\jp
mkdir content\en
```

`jp`および`en`下に`_index.md`ファイルを作成して以下のように編集し、文字コードUTF-8で保存します。
※ディレクトリのファイル名はASCII文字で記述してください。日本語ファイル名をつけるとヘルプが正常にビルドされません。

```md
---
title: "My Help"
---

# My Help

My Help サイトです。

```

```md
---
title: "My Help"
---

# My Help

This is My Help.

```

### 記事の作成

次に記事を作成します。`jp`下に`Hello.md`を作成して以下のように編集します。
※記事のファイル名はASCII文字で記述してください。日本語ファイル名をつけるとヘルプが正常にビルドされません。

```md
---
title: "Hello"
weight: 10
---

## Hello

こんにちは。
```

```md
---
title: "Hello"
weight: 10
---

## Hello

Hello World !
```

### サイトをプレビューする

ここまでで一度プレビューしてみましょう。以下のコマンドを実行し、ブラウザで`http://localhost:1313/`および`http://localhost:1314/`にアクセスします。

```
hugo server
```

`Hello.md`を適当に編集して保存してみます。LiveReloadがかかり、ブラウザ上のプレビューも更新されることを確認します。原稿はこのようにプレビューで執筆していきます。

### ディレクトリと2番目の記事を追加する

`jp`下に`second_content`ディレクトリを作成し、`_index.md`を作成して以下のように編集します。

```md
---
title: "Second Chapter"
chapter: true
weight: 20
---

# Second Chapter

2番目のチャプターです。
```

また`second.md`を作成して以下のように編集して保存します。

```md
---
title: "2番目の記事"
weight: 10
---

## 2番目の記事

こんにちは。これは2番目の記事です。
```

再度プレビューを表示し、2番目の記事が表示されることを確認します。

```
hugo server
```

## ヘルプの作成

`Ctrl+C` でhugoのプレビューを終了し、以下のコマンドを実行してヘルプをビルドします。

```
.\CI\build.bat
```

* `myhelp\public_chm\jp\MyHelp.chm`
* `myhelp\public_chm\en\MyHelp.chm`

が成果物のヘルプです。確認してみましょう。