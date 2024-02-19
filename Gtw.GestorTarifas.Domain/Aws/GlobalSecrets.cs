﻿using Gtw.GestorTarifas.Domain.Models.Configuracao;
using System;

namespace Gtw.GestorTarifas.Domain.Aws
{
    public static class GlobalSecrets
    {
        private static string _elasticUri;
        public static string ElasticSearchUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_elasticUri))
                {
                    _elasticUri = SecretsManager.GetSecretValueAsync("MAXIMA-ELASTICSEARCH-LOGGING").Result;
                }

                return _elasticUri;
            }
        }

        private static WiseConsActions wiseConsActions;
        public static WiseConsActions WiseConsActions
        {
            get
            {
                if (wiseConsActions == null)
                {
                    var secret = SecretsManager.GetSecretValueAsync("EP-APITARIFADOR-WISECONSAPI").Result;
                    if (!string.IsNullOrEmpty(secret))
                    {
                        wiseConsActions = Newtonsoft.Json.JsonConvert.DeserializeObject<WiseConsActions>(secret);
                    }
                }

                return wiseConsActions;
            }
        }

        private static ChavesGtwApiGestorTarifas chavesGtwApiGestorTarifas;
        public static ChavesGtwApiGestorTarifas ChavesGtwApiGestorTarifas
        {
            get
            {
                if (chavesGtwApiGestorTarifas == null)
                {
                    var secret = SecretsManager.GetSecretValueAsync("GTW-GESTORTARIFAS-API-KEYS").Result;
                    if (!string.IsNullOrEmpty(secret))
                    {
                        chavesGtwApiGestorTarifas = Newtonsoft.Json.JsonConvert.DeserializeObject<ChavesGtwApiGestorTarifas>(secret);
                    }
                }

                return chavesGtwApiGestorTarifas;
            }
        }
    }
}