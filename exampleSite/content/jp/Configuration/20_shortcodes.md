---
title: ショートコード
weight: 20
---

mdhelpテーマで使用できるショートコードの一覧です。これらはビルドされる HTML Help に対しても有効です。

## ShowIf

`config.toml`の`showIfs`で列挙されている場合に描画する部分を指定します。以下は`showIfs = ["editionA"]`とした場合に描画されるブロックです。

```
{{%/* ShowIf editionA */%}}
ここにxxxをサポートする場合に表示するコンテンツを記述。
{{%/* /ShowIf */%}}
```

[構成](./10_ConfigureHelp.html#showIfs)も参照してください。

## HideIf

`config.toml`の`showIfs`で列挙されている場合に描画「しない」部分を指定します。

```
{{%/* HideIf editionA */%}}
ここはeditionAでは表示したくない。
{{%/* /HideIf */%}}
```

[構成](./10_ConfigureHelp.html#showIfs)も参照してください。

## note

注記です。以下のように`note`ショートコードで囲まれた部分が注記としてレンダリングされます。

```
{{%/* note */%}}
ここに注記文章を記載
{{%/* /note */%}}
```
{{% note %}}
ここに注記文章を記載
{{% /note %}}

`note (title)`と言う形式で、引数にタイトルを指定することもできます。note内部にMarkdownを書くことも可能です。

```
{{%/* note 注記 */%}}
ここに注記文章を記載

* markdownも記載可能
  * 箇条書きレベル2
* 箇条書きレベル1
{{%/* /note */%}}
```



{{% note 注記 %}}
ここに注記文章を記載

* markdownも記載可能
  * 箇条書きレベル2
* 箇条書きレベル1
{{% /note %}}
