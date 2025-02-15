import { globalStyles } from "@/components/common/GlobalStyles";
import CustomTab from "@/components/common/Tab";
import { useTranslation } from "react-i18next";
import { View, StyleSheet, Platform } from "react-native";
import RecipesTab from "./RecipesTab";
import CommentsTab from "./CommentsTab";
import { Dispatch, RefObject, SetStateAction } from "react";
import { BottomSheetMethods } from "@gorhom/bottom-sheet/lib/typescript/types";

type BodyProps = {
  accountId: string;
  bottomSheetRef: RefObject<BottomSheetMethods>;
  bottomSheetCommentRef: RefObject<BottomSheetMethods>;
  setCurrentRecipeId: Dispatch<SetStateAction<string>>;
  setCurrentAuthorId: Dispatch<SetStateAction<string>>;
  setCurrentComment: Dispatch<SetStateAction<CommentCustomType>>;
  setCurrentCommentAuthorId: Dispatch<SetStateAction<string>>;
};

const Body = ({
  accountId,
  bottomSheetRef,
  bottomSheetCommentRef,
  setCurrentAuthorId,
  setCurrentRecipeId,
  setCurrentComment,
  setCurrentCommentAuthorId
}: BodyProps) => {
  const { t } = useTranslation("profile");

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
    <View className={`h-full ${Platform.select({ ios: "", android: "flex-1" })}`}>
      <CustomTab
        variant='primary'
        tabItems={tabs}
        tabViews={[
          <RecipesTab
            key='RecipesTab'
            accountId={accountId}
            bottomSheetRef={bottomSheetRef}
            setCurrentRecipeId={setCurrentRecipeId}
            setCurrentAuthorId={setCurrentAuthorId}
          />,
          <CommentsTab
            key='CommentsTab'
            accountId={accountId}
            bottomSheetCommentRef={bottomSheetCommentRef}
            setCurrentComment={setCurrentComment}
            setCurrentCommentAuthorId={setCurrentCommentAuthorId}
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
