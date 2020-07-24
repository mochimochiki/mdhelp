---
title: ヘルプの構成
weight: 10
---

ヘルプを構成するための`config.toml`の主な項目の説明をします。[HUGO公式ドキュメント](https://gohugo.io/getting-started/configuration/)も合わせて参照してください。

## Config

### publishDir

出力ディレクトリです。`public_(environment名)`と設定します。例えば`chm`ディレクトリにconfig.tomlを配置している環境では`public_chm`と設定します。

### ignoreFiles

無視するファイル。エディションごとで出力ディレクトリやファイルを変更する場合に使用します。例えば`ignoreFiles = ["ignore"]`とすると`ignore`の含まれるディレクトリ・ファイルがビルド対象外（ヘルプの目次作成からも対象外）になります。

## Config.params

\[params\]以下に設定する設定値です。

### author

フッターに表示されるauthorを設定します。

### copyRightYear

フッターに表示されるCopyRightの年を設定します。

### isCHM

CHMかどうか。この設定は通常Webサイト用のconfig.tomlでは`false`とし、HTML Help生成用のconfig.tomlでは`true`にします。テンプレートを使用してWebサイト生成時とヘルプ生成時で処理を分けるために使用します。

### custom_css

独自のcssを利用する場合に設定します。例えば`/static/css/custom.css`にcssファイルを配置した場合、`custom_css = ["/css/custom.css"]`とするとcssファイルが適用されます。

### custom_js

独自のjavascriptを利用する場合に設定します。例えば`/static/js/custom.js`にjsファイルを配置した場合、`custom_js = ["/js/custom.js"]`とするとjavascriptが適用されます。

### showIfs

`ShowIf`ショートコードで表示する条件。以下は`showIfs = ["supportFuncA"]`とした場合に描画されるブロックです。

```
{{%/* ShowIf supportFuncA */%}}
ここにxxxをサポートする場合に表示するコンテンツを記述。
{{%/* /ShowIf */%}}
```

## Config.languages

[languages] - [languages.jp] などに設定する設定値です。

### title

各言語のヘルプタイトルです。ヘルプのファイル名称およびウィンドウタイトルに利用されます。

## フロントマター

記事のフロントマターでの設定は以下のとおりです。

### hideHelpTOC

チャプターのインデックスページをヘルプの目次（ツリーの本アイコン部分）クリックで表示するかどうか。デフォルトでは隠さず表示されます。隠す場合は隠したいチャプターの`_index.md`のフロントマターに以下のように`hideHelpTOC`を追加します。

```
hideHelpTOC: true
```