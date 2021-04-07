---
title: エディション
weight: 30
---

内容が微妙に異なるヘルプのファミリーを生成する必要がある場合、HUGOの[Configuration Directory](https://gohugo.io/getting-started/configuration/#configuration-directory)の仕組みを利用してエディションを作り分け、構成や記事内容を切り替えてビルドできます。

## エディションのconfigを作成する

`/config/chm_other`というディレクトリを作成し、`/config/chm/config.toml`をコピーして編集することで簡単に`chm_other`エディションを作成することができます。それぞれのエディションのconfigは[ヘルプの構成](./30_Edition.html)に沿って編集します。以下では主要な設定変更について説明します。

### タイトル

ヘルプのタイトルがエディションごとに違う場合、各言語の`title`を設定します。

```
[languages]
  [languages.jp]
    title = "other"
```

### 特定記事・特定要素の表示/非表示を切り替える

[ShowIf](./20_shortcodes.html),[HideIf](./20_shortcodes.html)ショートコード/フロントマターと共に`showIfs`を使用することで表示/非表示を切り替えます。

```toml
[params]
  showIfs = ["editionA"]
```

この場合、`.md`の記事で以下のように記載した要素が表示されます。

```
{{%/* ShowIf editionA */%}}
ここにeditionAをサポートする場合に表示するコンテンツを記述。
{{%/* /ShowIf */%}}
```

以下のように記載し作用素は表示されません。

```
{{%/* HideIf editionA */%}}
ここにeditionAの場合に非表示とするコンテンツ。
{{%/* /HideIf */%}}
```

記事単位では以下のようにフロントマターを書いている場合にchmに含まれます。一つのエディションだけであってもリスト形式（`["editionA"]`）で書く必要があります。`HideIf`も同様です。

```
---
title: editionAの説明
ShowIf: ["editionA"]
---
```

## ビルドする

エディションを指定してビルドするには、引数にエディションを指定します。例えば`chm_other`エディションをビルドするには以下のように指定します。

```
.\CI\build.bat chm_other
```