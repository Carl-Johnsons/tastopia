import { useTranslation } from "react-i18next";
import { useState } from "react";
import { View, Text, StyleSheet, FlatList, TouchableOpacity } from "react-native";
import { AntDesign } from "@expo/vector-icons";

import { UNIT } from "@/constants/unit";
import { UnitTypeCode, useGetUnitsByUnitTypeCodeQuery } from "@/generated/schema";
import PagoDropDown from "../PagoDropDown";
import PagoButton from "../PagoButton";
import { globalStyles } from "../GlobalStyles";
import { ViewIadWareHousePlantItemWithIndex } from "../Screens/InventoryManagement/type";

type UnitProps = {
  viewIadWarehousePlantItems: ViewIadWareHousePlantItemWithIndex[];
  handleConverseUnitChange: (listConversed: ViewIadWareHousePlantItemWithIndex[]) => void;
  triggerUnitInputChange: (unit: string) => void;
};

const Unit = ({
  viewIadWarehousePlantItems,
  handleConverseUnitChange,
  triggerUnitInputChange
}: UnitProps) => {
  const { t } = useTranslation("inventoryManagement");

  const [showDropDown, setShowDropDown] = useState(false);

  const {
    data: unitPlantMeasureDataResponse,
    loading: unitPlantMeasureDataResponseLoading
  } = useGetUnitsByUnitTypeCodeQuery({
    variables: { unitType: { code: UnitTypeCode.PlantMeasure } }
  });

  const handleSelectUnitChange = (unitCodeSelected: string) => {
    const unitConversions = {
      [UNIT.TAN]: {
        name: t("plantItemPage.total.tan"),
        factor: { [UNIT.KG]: 0.001, [UNIT.QUINTAL]: 0.1, [UNIT.PLANT]: 1 }
      },
      [UNIT.QUINTAL]: {
        name: t("plantItemPage.total.quintal"),
        factor: { [UNIT.KG]: 0.01, [UNIT.TAN]: 10, [UNIT.PLANT]: 1 }
      },
      [UNIT.KG]: {
        name: t("plantItemPage.total.Kg"),
        factor: { [UNIT.TAN]: 1000, [UNIT.QUINTAL]: 100, [UNIT.PLANT]: 1 }
      },
      [UNIT.PLANT]: {
        name: t("plantItemPage.total.plant"),
        factor: { [UNIT.TAN]: 1, [UNIT.QUINTAL]: 1, [UNIT.KG]: 1 }
      }
    };

    const conversion = unitConversions[unitCodeSelected];
    if (!conversion) {
      alert("Invalid unit!");
      return;
    }

    const conversedList = viewIadWarehousePlantItems.map(item => {
      if (item.unitCode !== unitCodeSelected && item.unitCode !== "CAY") {
        item.currentStock = parseFloat(
          (item.currentStock * conversion.factor[item.unitCode]).toPrecision(4)
        );
        item.unitName = conversion.name;
        item.unitCode = unitCodeSelected;
      }
      return item;
    });
    triggerUnitInputChange(unitConversions[unitCodeSelected].name);
    handleConverseUnitChange(conversedList);
  };

  return (
    <View style={styles.unitWrapper}>
      <Text style={styles.title}>{t("plantItemPage.dataTable.columnData.unit")}</Text>

      {!unitPlantMeasureDataResponseLoading && (
        <PagoDropDown
          onCancel={() => {
            setShowDropDown(false);
          }}
          show={showDropDown}
          listStyle={{ width: 90, backgroundColor: "#fff" }}
          align='right'
          renderBtn={() => (
            <PagoButton
              buttonStyle={styles.btnStyle}
              title={""}
              color='primary'
              onPress={() => {
                setShowDropDown(true);
              }}
              leftIcon={
                <AntDesign
                  style={{ paddingLeft: 10 }}
                  name='caretdown'
                  size={10}
                  color='white'
                />
              }
            />
          )}
          renderDropDownContent={() => {
            return (
              <FlatList
                data={unitPlantMeasureDataResponse?.getUnitsByUnitTypeCode!}
                renderItem={({ item }) => (
                  <TouchableOpacity onPress={() => handleSelectUnitChange(item.code)}>
                    <View style={styles.itemWrapper}>
                      <Text style={styles.itemText}>{item.name}</Text>
                    </View>
                  </TouchableOpacity>
                )}
                keyExtractor={item => item.code}
              />
            );
          }}
        />
      )}
    </View>
  );
};

export default Unit;

const styles = StyleSheet.create({
  unitWrapper: {
    flexDirection: "row",
    alignItems: "center",
    paddingVertical: 10
  },
  title: {
    color: globalStyles.color.secondary,
    fontWeight: "600",
    fontSize: 15,
    marginRight: 6
  },
  btnStyle: {
    width: 38
  },
  itemWrapper: {
    padding: 6
  },
  itemText: {
    textAlign: "center"
  }
});
