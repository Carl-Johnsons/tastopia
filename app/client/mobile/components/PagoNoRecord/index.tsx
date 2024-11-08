import { Image, Text, View } from "react-native";
import styles from "./PagoNoRecord.style";
import React from "react";
import { useTranslation } from "react-i18next";

export default function PagoNoRecord() {
  const { t } = useTranslation("component");

  return (
    <View style={styles.container}>
      <Image
        source={require("../../assets/images/empty.png")}
        style={styles.image}
      />
      <Text style={styles.noData}>{t("noData")}</Text>
    </View>
  );
}
