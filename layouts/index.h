{{- define "create_alias" -}}
{{- range .Pages -}}
{{- if .OutputFormats.Get "html" }}
{{- $id := replace .RelPermalink "/" "_" -}}
{{- $id = replace $id ".html" "" | upper -}}
#define IDH_{{ $id }} xxxxx
{{ chomp "\r\n"}}
{{- if .IsSection -}}
{{ template "create_alias" . }}
{{- end -}}
{{- end -}}
{{- end -}}
{{- end -}}
{{- template "create_alias" .Site.Home }}