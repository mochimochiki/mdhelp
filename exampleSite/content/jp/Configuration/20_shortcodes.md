---
title: ショートコード
weight: 20
---

## ショートコード

### ShowIf

`config.toml`の`showIfs`で列挙されている場合に描画する部分を指定します。以下は`showIfs = ["supportFuncA"]`とした場合に描画されるブロックです。

```
{{%/* ShowIf supportFuncA */%}}
ここにxxxをサポートする場合に表示するコンテンツを記述。
{{%/* /ShowIf */%}}
```

[構成](./10_ConfigurationHelp.html#showIfs)も参照してください。