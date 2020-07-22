---
title: エディション
weight: 30
---

## エディション

内容が微妙に異なるヘルプのファミリーを生成する必要がある場合、エディションを分けてビルドすることができます。

### 違うエディションのconfigを作成する

`/config/chm_other_version`というディレクトリを作成し、`/config/chm/config.toml`をコピーして編集することで。`chm_other_version`エディションを作成することができます。それぞれのエディションのconfigで以下の設定を適切に設定します。

### ディレクトリ/ファイルをエディションによって無視する

`config.toml``ignoreFiles = ["ignore"]`

### 記事中の特定要素の表示/非表示を切り替える

[ShowIf](./20_shortcodes.html)ショートコードとconfigの`showIfs`を使用することで表示/非表示を切り替えます。例えば以下の設定がされているエディション

```
[params]
  showIfs = ["supportFuncA"]
```

この場合、以下のように記載した要素が表示されます。

```
{{%/* ShowIf supportFuncA */%}}
ここにsupportFuncAをサポートする場合に表示するコンテンツを記述。
{{%/* /ShowIf */%}}
```