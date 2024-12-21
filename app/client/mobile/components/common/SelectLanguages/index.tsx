import { StyleSheet, ImageSourcePropType } from "react-native";
import { SelectCountry } from "react-native-element-dropdown";
import i18next from "i18next";

import { globalStyles } from "../GlobalStyles";

const SelectLanguages = () => {
  const languageOptions: {
    label: string;
    language: string;
    image: ImageSourcePropType | undefined;
  }[] = [
    {
      label: "",
      language: "vi",
      image: require("../../assets/images/languages/vn-flag.png")
    },
    {
      label: "",
      language: "en",
      image: require("../../assets/images/languages/kingdom-flag.png")
    }
  ];
  const currentLanguage = languageOptions.filter(
    ip => ip.language === i18next.language
  )?.[0];

  const handleChangeLanguage = (language: string) => {
    i18next.changeLanguage(language);
  };

  return (
    <SelectCountry
      style={styles.dropDown}
      iconStyle={styles.dropDownIcon}
      containerStyle={styles.selectList}
      imageStyle={styles.image}
      activeColor={globalStyles.color.primary}
      data={languageOptions}
      labelField='label'
      valueField='language'
      imageField='image'
      value={currentLanguage.language}
      onChange={({ language }) => {
        handleChangeLanguage(language);
      }}
    />
  );
};

export default SelectLanguages;

const styles = StyleSheet.create({
  dropDown: {
    width: 40,
    height: 40,
    borderColor: "black",
    borderWidth: 1,
    borderRadius: 5,
    display: "flex",
    flexDirection: "row",
    paddingLeft: 2
  },

  dropDownIcon: {
    display: "none"
  },

  selectList: {
    borderColor: globalStyles.color.secondaryOpacity,
    borderWidth: 1,
    borderRadius: 10,
    overflow: "hidden"
  },

  image: {
    width: 26,
    height: 26
  }
});
