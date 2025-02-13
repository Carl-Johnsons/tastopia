import { globalStyles } from "@/components/common/GlobalStyles";
import CustomTab from "@/components/common/Tab";
import { TabView } from "@rneui/themed";
import { useTranslation } from "react-i18next";
import { Text, View, StyleSheet, SafeAreaView, Platform } from "react-native";
import RecipesTab from "./RecipesTab";
import CommentsTab from "./CommentsTab";

type BodyProps = {
  accountId: string;
};

const Body = ({ accountId }: BodyProps) => {
  const { t } = useTranslation("");

  const tabs = [
    {
      title: t("recipes"),
      titleStyle: styles.tabTitle
    },
    {
      title: t("comments"),
      titleStyle: styles.tabTitle
    }
  ];

  return (
    <View className='h-full flex-1'>
      <CustomTab
        variant='primary'
        tabItems={tabs}
        tabViews={[
          <RecipesTab
            key='RecipesTab'
            accountId={accountId}
          />,
          <CommentsTab
            key='CommentsTab'
            accountId={accountId}
          />
        ]}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  tabTitle: {
    fontSize: 16,
    fontWeight: "700",
    width: 160,
    height: Platform.select({ ios: 30, android: 40 }),
    textAlignVertical: "center"
  },
  header: {
    padding: 20,
    width: "100%",
    flexDirection: "row",
    justifyContent: "space-between",
    backgroundColor: globalStyles.color.primary
  },
  title: {
    fontSize: 16,
    fontWeight: "bold",
    color: globalStyles.color.light
  }
});

export default Body;
